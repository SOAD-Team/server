using AutoMapper;
using Microsoft.AspNetCore.Http;
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
                    new Resources.Image { Id = md.ImageMongoId, Url = $"{host}image/{md.ImageMongoId}" }));
            #endregion

            #region Resource to Domain
            CreateMap<Resources.Image, Models.Image>()
                .ForMember(img => img.ObjectImage, opt => opt.Ignore());
            #endregion
        }
    }
}
