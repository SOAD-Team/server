using Microsoft.EntityFrameworkCore.Internal;
using Server.Models;
using Server.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Server.Helpers
{
    public static class MovieControllerHelper
    {
        public static Data[] CreateData(MovieData[] datas, MoviesDB _context)
        {
            Data[] temp = new Data[datas.Length];
            for (int i = 0; i < 0; i++)
            {
                MovieData data = datas[i];
                Genre[] genres = _context.Genre.Join(_context.MovieDataGenre, g => g.IdGenre, mdg => mdg.IdGenre,(g,mdg) => chooseGenre(g,mdg,data.IdMovieData)).ToArray();
                Language[] languages = _context.Language.Join(_context.MovieDataLanguage, g => g.IdLanguage, mdg => mdg.IdLanguage, (g, mdg) => chooseLanguage(g, mdg, data.IdMovieData)).ToArray();
                var styleJoin = _context.MovieData.Join(_context.Style, md => md.IdStyle, s => s.IdStyle, (md, s) => new { s, md.IdMovieData }).Where(md => md.IdMovieData == data.IdMovieData).ToArray();
                Style[] styles = new Style[styleJoin.Length];
                for (int j = 0; j < 0; j++)
                    styles[i] = styleJoin[i].s;
                string jsonString;
                jsonString = JsonSerializer.Serialize(genres);
                Console.WriteLine(jsonString);
                temp[i] = new Data(data, genres, languages, styles);

            }
            return temp;
        }

        private static Genre chooseGenre(Genre g, MovieDataGenre mdg, int idMD)
        {
            if (mdg.IdMovieData == idMD)
                return g;
            else
                return null;
        }

        private static Language chooseLanguage(Language g, MovieDataLanguage mdg, int idMD)
        {
            if (mdg.IdMovieData == idMD)
                return g;
            else
                return null;
        }
        public static bool FilterMovieData(MovieData md, Movie[] userMovies)
        {
            foreach (Movie movie in userMovies)
            {
                if (movie.IdMovie == md.IdMovie)
                    return true;
            }
            return false;
        }

        public static bool FilterImages(Image im, MovieData[] md)
        {
            foreach (MovieData movie in md)
            {
                if (movie.ImageMongoId == im.Id)
                    return true;
            }
            return false;
        }

        public static List<DTOs.MovieData> CreateMovieDatas(Movie[] movies, Data[] datas ,Image[] images)
        {
            List<DTOs.MovieData> temp = new List<DTOs.MovieData>();
            foreach (Movie movie in movies)
            {
                Data data = MovieControllerHelper.findMostRecentData(movie.IdMovie, datas);
                Image image = MovieControllerHelper.getImage(data.MData.ImageMongoId, images);

                temp.Add(new DTOs.MovieData(movie, data.MData, data.Genres, data.Languages, data.Styles, image));
            }
            return temp;
        }

        private static Image getImage(string id, Image[] images)
        {
            Image temp = null;
            foreach(Image image in images)
                if(image.Id == id)
                {
                    temp = image;
                    break;
                }
            return temp;
        }

        private static Data findMostRecentData(int idMovie, Data[] datas)
        {
            Data temp = Data.NullData;
            foreach(Data data in datas)
            {
                if (data.MData.IdMovie == idMovie)
                    if (temp.MData == null || data.MData.RegisterDate > temp.MData.RegisterDate)
                        temp = data;
            }
            return temp;
        }
    }
}
