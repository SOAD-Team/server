using Server.Models;
using Server.Structs;
using System.Collections.Generic;
using System.Linq;
using Server.Persistence;
using AutoMapper;

namespace Server.Helpers
{
    public static class MovieControllerHelper
    {
        public static Data[] CreateData(MovieData[] datas, MoviesDB _context, IMapper mapper)
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



                temp[i] = new Data(data, mapper.Map<IEnumerable<Genre>, IEnumerable<Resources.KeyValuePair>>(genres).ToArray(), mapper.Map<IEnumerable<Language>, IEnumerable<Resources.KeyValuePair>>(languages).ToArray(), mapper.Map<IEnumerable<Style>, IEnumerable<Resources.KeyValuePair>>(styles).ToArray());

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

        public static List<Resources.Movie> CreateMovieDatas(Data[] datas, Image[] images, int idUser)
        {
            List<Resources.Movie> temp = new List<Resources.Movie>();
            foreach (Data data in datas)
            {
                Image image = MovieControllerHelper.getImage(data.MData.ImageMongoId, images);

                temp.Add(new Resources.Movie(data.MData.IdMovie, idUser, data.MData, data.Genres, data.Languages, data.Styles, image));
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

        public static List<MovieData> FilterMovieDataByUser(List<MovieData> movies, MoviesDB _context, int idUser)
        {
            List<MovieData> filtred = new List<MovieData>();
            foreach (MovieData movie in movies)
            {
                int movieUserId = _context.Movie.Where(val => val.IdMovie == movie.IdMovie).FirstOrDefault().IdUser;
                if (movieUserId == idUser)
                    filtred.Add(movie);
            }
                
            return filtred;
        }

        public static List<MovieData> FilterMovieDataByMovie(IEnumerable<MovieData> movies, int idMovie)
        {
            List<MovieData> filtred = new List<MovieData>();
            foreach (MovieData movie in movies)
            {
                if (movie.IdMovie == idMovie)
                    filtred.Add(movie);
            }

            return filtred;
        }

        public static List<MovieData> GetMostRecentData(IEnumerable<MovieData> movies, MoviesDB _context)
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

        public static Resources.Movie CreateMovieDataOnDb(MoviesDB _context, int movieId, Resources.Movie movieData, IImagesDB _mongoContext, IMapper mapper)
        {
            Movie movie = _context.Movie.Find(movieId);
            MovieData data = movieData.MapToModel(movieId, movieData.Image.Id);
            _context.MovieData.Add(data);
            _context.SaveChanges();

            foreach (Resources.KeyValuePair genre in movieData.Genres)
            {
                if (genre.Id.Equals(null))
                {
                    _context.Genre.Add(new Genre(genre.Name));
                    _context.SaveChanges();
                }
                _context.MovieDataGenre.Add(new MovieDataGenre(data.IdMovieData, genre.Id));
            }
            foreach (Resources.KeyValuePair language in movieData.Languages)
            {
                if (language.Id.Equals(null))
                {
                    _context.Language.Add(new Language(language.Name));
                    _context.SaveChanges();
                }
                _context.MovieDataLanguage.Add(new MovieDataLanguage(data.IdMovieData, language.Id));
            }

            _context.SaveChanges();
            var qGenres = _context.Genre.Join(_context.MovieDataGenre, g => g.IdGenre, mdg => mdg.IdGenre, (g, mdg) => new { g.IdGenre, g.Name, mdg.IdMovieData }).Where(g => g.IdMovieData == data.IdMovieData).ToList();
            List<Genre> genres = new List<Genre>();
            qGenres.ForEach(g => genres.Add(new Genre(g.IdGenre, g.Name)));
            List<Language> languages = new List<Language>();
            var qLanguages = _context.Language.Join(_context.MovieDataLanguage, g => g.IdLanguage, mdg => mdg.IdLanguage, (g, mdg) => new { g.IdLanguage, g.Name, mdg.IdMovieData }).Where(g => g.IdMovieData == data.IdMovieData).ToList();
            qLanguages.ForEach(g => languages.Add(new Language(g.IdLanguage, g.Name)));
            Style[] styles = _context.Style.Where(s => s.IdStyle == data.IdStyle).ToArray<Style>();

            return data.MapToPresentationModel(movie.IdUser, genres.ToArray(), languages.ToArray(), _mongoContext, styles, mapper);
        }
    }
}
