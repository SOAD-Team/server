using AutoMapper;
using Server.Helpers;
using Server.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile(
            IMovieRepository movieRepository,
            IGenreRepository genreRepository,
            ILanguageRepository languageRepository,
            IStyleRepository styleRepository,
            IReviewRepository reviewRepository,
            IMovieDataRepository movieDataRepository,
            IMovieDataGenreRepository movieDataGenreRepository,
            IMovieDataLanguageRepository movieDataLanguageRepository,
            IUnitOfWork unitOfWork)
        {
            string host = Environment.GetEnvironmentVariable("URL");
            CreateMap<Models.Review, Resources.Review>();
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
                .ForMember(img => img.Url, opt => opt.MapFrom(img => $"{host}image/{img.Id}"));

            CreateMap<Models.Movie, Resources.Movie>()
                .ForMember(m => m.IdUser, opt => opt.MapFrom(m => m.IdUser))
                .ForMember(m => m.IdMovie, opt => opt.MapFrom(m => m.IdMovie))
                .AfterMap((movieModel, movie) =>
                {
                    var data = movieDataRepository.GetByMovieId(movieModel.IdMovie).Result;
                    var genres = new List<Resources.KeyValuePair>();
                    var languages = new List<Resources.KeyValuePair>();

                    movie.IdMovieData = data.IdMovieData;
                    movie.RegisterDate = data.RegisterDate;
                    movie.Name = data.Title;
                    movie.Year = data.Year;
                    movieDataGenreRepository.GetAll().Result.Where(genre => genre.IdMovieData == data.IdMovieData)
                        .Join(genreRepository.GetAll().Result, mdg => mdg.IdGenre, g => g.IdGenre, (mdg, g) => g)
                        .ToList().ForEach(genre => {
                            genres.Add(new Resources.KeyValuePair { Id = genre.IdGenre, Name = genre.Name });
                        });

                    movieDataLanguageRepository.GetAll().Result.Where(language => language.IdMovieData == data.IdMovieData)
                        .Join(languageRepository.GetAll().Result, mdl => mdl.IdLanguage, l => l.IdLanguage, (mdg, l) => l)
                        .ToList().ForEach(language => {
                            languages.Add(new Resources.KeyValuePair { Id = language.IdLanguage, Name = language.Name });
                        });
                    movie.PlatFav = data.PlatFav;
                    movie.Image = new Resources.Image {Id = data.ImageMongoId, Url = $"{host}image/{data.ImageMongoId}" };
                    movie.Styles = new Resources.KeyValuePair[1] 
                    { new Resources.KeyValuePair { Id = data.IdStyle, Name = styleRepository.Get(data.IdStyle).Result.Name} };
                    movie.MetaScore = data.MetaScore;
                    movie.Imdb = data.Imdb;
                    movie.Director = data.Director;
                    movie.Popularity = ScoreHelper.GetMoviePopularity(movie.IdMovie.Value, movieDataRepository, reviewRepository);
                    movie.CommunityScore = ScoreHelper.GetMovieCommunityScore(movie.IdMovie.Value, reviewRepository);

                    movie.Genres = genres.ToArray();
                    movie.Languages = languages.ToArray();
                });
            CreateMap<Models.MovieData, Resources.Movie>()
                .ForMember(m => m.Name, opt => opt.MapFrom(md => md.Title))
                .ForMember(m => m.Styles, opt => opt.MapFrom(md => 
                    new Resources.KeyValuePair[1] { 
                        new Resources.KeyValuePair { Id = md.IdStyle, Name = styleRepository.GetAll().Result
                            .Where(s => s.IdStyle == md.IdStyle).FirstOrDefault().Name } }))
                .ForMember(m => m.Image, opt => opt.MapFrom(md => 
                    new Resources.Image { Id = md.ImageMongoId, Url = $"{host}image/{md.ImageMongoId}" }))
                .ForMember(m => m.CommunityScore,opt => opt.MapFrom(md => ScoreHelper.GetMovieCommunityScore(md.IdMovie, reviewRepository)))
                .ForMember(m => m.CommunityScore, opt => opt.MapFrom(md => ScoreHelper.GetMoviePopularity(md.IdMovie, movieDataRepository, reviewRepository)))
                .AfterMap((movieData, movie) =>
                {
                    var genres = new List<Resources.KeyValuePair>();
                    var languages = new List<Resources.KeyValuePair>();
                    
                    movie.IdUser = movieRepository.Get(movieData.IdMovie).Result.IdUser;

                    movieDataGenreRepository.GetAll().Result.Where(genre => genre.IdMovieData == movieData.IdMovieData)
                        .Join(genreRepository.GetAll().Result, mdg => mdg.IdGenre, g => g.IdGenre, (mdg, g) => g)
                        .ToList().ForEach(genre => {
                            genres.Add(new Resources.KeyValuePair { Id = genre.IdGenre, Name = genre.Name });
                        });

                    movieDataLanguageRepository.GetAll().Result.Where(language => language.IdMovieData == movieData.IdMovieData)
                        .Join(languageRepository.GetAll().Result, mdl => mdl.IdLanguage, l => l.IdLanguage, (mdg, l) => l)
                        .ToList().ForEach(language => {
                            languages.Add(new Resources.KeyValuePair { Id = language.IdLanguage, Name = language.Name });
                        });

                    movie.Genres = genres.ToArray();
                    movie.Languages = languages.ToArray();
                });     
            #endregion

            #region Resource to Domain
            CreateMap<Resources.Movie, Models.Movie>()
                .ForMember(m => m.IdMovie, opt => opt.MapFrom(m => m.IdMovie))
                .ForMember(m => m.IdUser, opt => opt.MapFrom(m => m.IdUser));
            CreateMap<Resources.Movie, Models.MovieData>().BeforeMap((movie, movieData) =>
            {
                foreach (Resources.KeyValuePair genre in movie.Genres)
                {
                    if (genre.Id.Equals(null))
                    {
                        genreRepository.Create(new Models.Genre(genre.Name));
                        unitOfWork.CompleteAsync().Wait();
                    }
                }
                foreach (Resources.KeyValuePair language in movie.Languages)
                {
                    if (language.Id.Equals(null))
                    {
                        languageRepository.Create(new Models.Language(language.Name));
                        unitOfWork.CompleteAsync().Wait();
                    }
                }
            }).ForMember(m => m.ImageMongoId, opt => opt.MapFrom(m => m.Image.Id))
            .ForMember(m => m.Title, opt=> opt.MapFrom(m=> m.Name))
            .ForMember(m => m.IdStyle, opt=> opt.MapFrom(m=> m.Styles[0].Id));

            CreateMap<Resources.Image, Models.Image>()
                .ForMember(img => img.ObjectImage, opt => opt.Ignore())
                .ForMember(img => img.Id, opt => opt.MapFrom(img => img.Id));
            #endregion
        }

    }
}
