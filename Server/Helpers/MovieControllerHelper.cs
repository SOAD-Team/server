using Server.Models;
using Server.Structs;
using System.Collections.Generic;
using System.Linq;

namespace Server.Helpers
{
    public static class MovieControllerHelper
    {
        public static Data[] CreateData(MovieData[] datas, MoviesDB _context)
        {
            Data[] temp = new Data[datas.Length];
            for (int i = 0; i < datas.Length; i++)
            {
                MovieData data = datas[i];
                var genresInfo = _context.Genre.Join(_context.MovieDataGenre, g => g.IdGenre, mdg => mdg.IdGenre, (g, mdg) => new { g, mdg }).Where(val => val.mdg.IdMovieData == data.IdMovieData).ToArray();
                var genresList = new List<Genre>();
                foreach (var genre in genresInfo)
                {
                    genre.g.MovieDataGenre = null;
                    genresList.Add(genre.g);
                }
                var languagesInfo = _context.Language.Join(_context.MovieDataLanguage, g => g.IdLanguage, mdg => mdg.IdLanguage, (g, mdg) => new { g, mdg }).Where(val => val.mdg.IdMovieData == data.IdMovieData).ToArray();
                var languagesList = new List<Language>();
                foreach (var genre in languagesInfo)
                {
                    genre.g.MovieDataLanguage = null;
                    languagesList.Add(genre.g);
                }
                Genre[] genres = genresList.ToArray();
                Language[] languages = languagesList.ToArray();
                var styleJoin = _context.MovieData.Join(_context.Style, md => md.IdStyle, s => s.IdStyle, (md, s) => new { s, md.IdMovieData }).Where(md => md.IdMovieData == data.IdMovieData).ToArray();
                Style[] styles = new Style[styleJoin.Length];
                for (int j = 0; j < styleJoin.Length; j++)
                    styles[j] = styleJoin[j].s;

                temp[i] = new Data(data, genres, languages, styles);

            }
            return temp;
        }

        public static Image[] FilterImages(Image[] ims, MovieData[] md)
        {
            List<Image> temp = new List<Image>(); 
            foreach (MovieData movie in md)
            {
                foreach (Image im in ims)
                    if (movie.ImageMongoId == im.Id)
                    {
                        temp.Add(im);
                        break;
                    }
            }
            return temp.ToArray();
        }

        public static List<DTOs.MovieData> CreateMovieDatas(Data[] datas, Image[] images, int idUser)
        {
            List<DTOs.MovieData> temp = new List<DTOs.MovieData>();
            foreach (Data data in datas)
            {
                Image image = MovieControllerHelper.getImage(data.MData.ImageMongoId, images);

                temp.Add(new DTOs.MovieData(data.MData.IdMovie, idUser, data.MData, data.Genres, data.Languages, data.Styles, image));
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

        public static List<MovieData> FilterMovieData(List<MovieData> movies, MoviesDB _context)
        {
            List<MovieData> filtred = new List<MovieData>();

            foreach (var movie in movies)
            {
                List<MovieData> temp = _context.MovieData.Where(m => m.IdMovie == movie.IdMovie).ToList();

                MovieData add = null;
                foreach (var tempMovie in temp)
                {
                    if (add == null)
                    {
                        add = tempMovie;
                    }
                    else
                    {
                        if (add.RegisterDate < tempMovie.RegisterDate)
                        {
                            add = tempMovie;
                        }
                    }
                }

                if (filtred.Find(m => m.IdMovieData == add.IdMovieData) == null)
                {
                    filtred.Add(add);
                }

                add = null;
            }

            return filtred;
        }
    }
}
