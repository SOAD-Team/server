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

        public static Resources.Movie CreateMovieDataOnDb(MoviesDB _context, Resources.Movie movieData, IMapper mapper)
        {
            MovieData data = mapper.Map<MovieData>(movieData);
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

            return mapper.Map<Resources.Movie>(data);
        }
    }
}
