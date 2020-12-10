using AutoMapper;
using Microsoft.AspNetCore.Http;
using Server.Helpers;
using Server.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile(IImagesDB mongoContext, MoviesDB context)
        {
            string host = Environment.GetEnvironmentVariable("URL");
            #region Domain to Resource
            CreateMap<Models.User, Resources.User>();
            CreateMap<Models.Genre,Resources.KeyValuePair>()
                .ForMember(g => g.Id, opt => opt.MapFrom(gr => gr.IdGenre))
                .ForMember(g => g.Name, opt => opt.MapFrom(gr => gr.Name));
            CreateMap<Models.Language, Resources.KeyValuePair>()
                .ForMember(g => g.Id, opt => opt.MapFrom(gr => gr.IdLanguage))
                .ForMember(g => g.Name, opt => opt.MapFrom(gr => gr.Name));
            CreateMap<Models.Style, Resources.KeyValuePair>()
                .ForMember(g => g.Id, opt => opt.MapFrom(gr => gr.IdStyle))
                .ForMember(g => g.Name, opt => opt.MapFrom(gr => gr.Name));
            CreateMap<Models.Image, Resources.Image>()
                .ForMember(img => img.Url, opt => opt.MapFrom(img => $"{host}/image/{img.Id}"));

            CreateMap<Models.MovieData, Resources.Movie>()
                .ForMember(m => m.Name, opt => opt.MapFrom(md => md.Title))
                .ForMember(m => m.Genres, opt => opt.MapFrom(md => md.MovieDataGenre
                    .Join(context.Genre, mdg => mdg.IdGenre, g => g.IdGenre, (mdg, g) => g)))
                .ForMember(m => m.Languages, opt => opt.MapFrom(md => md.MovieDataLanguage
                    .Join(context.Genre, mdg => mdg.IdLanguage, g => g.IdGenre, (mdg, g) => g)))
                .ForMember(m => m.Styles, opt => opt.MapFrom(md => 
                    new Resources.KeyValuePair[1] { 
                        new Resources.KeyValuePair { Id = md.IdStyle, Name = context.Style
                            .Where(s => s.IdStyle == md.IdStyle).FirstOrDefault().Name } }))
                .ForMember(m => m.Image, opt => opt.MapFrom(md => 
                    new Resources.Image { Id = md.ImageMongoId, Url = $"{host}image/{md.ImageMongoId}" }))
                .ForMember(m => m.CommunityScore,opt => opt.MapFrom(md => RecommendationHelper.GetMovieCommunityScore(md.IdMovie,context)))
                .ForMember(m => m.CommunityScore, opt => opt.MapFrom(md => RecommendationHelper.GetMoviePopularity(md.IdMovie, context)));
            #endregion

            #region Resource to Domain
            CreateMap<Resources.Movie, Models.Movie>()
                .ForMember(m => m.IdMovie, opt => opt.MapFrom(m => m.IdMovie))
                .ForMember(m => m.IdUser, opt => opt.MapFrom(m => m.IdUser));
            CreateMap<Resources.Movie, Models.MovieData>().BeforeMap((movie, movieData) => {
                foreach (Resources.KeyValuePair genre in movie.Genres)
                {
                    if (genre.Id.Equals(null))
                    {
                        context.Genre.Add(new Models.Genre(genre.Name));
                        context.SaveChanges();
                    }
                }
                foreach (Resources.KeyValuePair language in movie.Languages)
                {
                    if (language.Id.Equals(null))
                    {
                        context.Language.Add(new Models.Language(language.Name));
                        context.SaveChanges();
                    }
                }
            }).AfterMap((movie, movieData) => {
                movieData.MovieDataGenre = new List<Models.MovieDataGenre>();
                movieData.MovieDataLanguage = new List<Models.MovieDataLanguage>();
                foreach (var genre in movie.Genres)
                    movieData.MovieDataGenre.Add(new Models.MovieDataGenre(movie.IdMovieData.Value, genre.Id));
                foreach (var language in movie.Languages)
                    movieData.MovieDataLanguage.Add(new Models.MovieDataLanguage(movie.IdMovieData.Value, language.Id));
            });
            CreateMap<Resources.Image, Models.Image>()
                .ForMember(img => img.ObjectImage, opt => opt.Ignore());
            #endregion
        }
    }
}
