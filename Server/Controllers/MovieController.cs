using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Models;
using System.Linq;
using System;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly MoviesDB _context;
        private readonly ImagesDB _mongoContext;

        public MovieController(ILogger<MovieController> logger, MoviesDB context, ImagesDB mongoContext)
        {
            _logger = logger;
            _context = context;
            _mongoContext = mongoContext;
        }

        [HttpPost]
        public DTOs.MovieData CreateMovie(DTOs.MovieData movieData)
        {
            Image image = movieData.Image.MapToImage();
            string imageId = _mongoContext.Create(image).Id;
            Movie movie = new Movie(movieData.IdUser);
            _context.Movie.Add(movie);
            _context.SaveChanges();
            int movieId = movie.IdMovie;
            _logger.Log(LogLevel.Information, "MovieId: " + movieId.ToString());
            MovieData data = movieData.MapToModel(movieId, imageId);
            _context.MovieData.Add(data);
            _context.SaveChanges();

            foreach (Genre genre in movieData.Genres)
            {
                if (genre.IdGenre.Equals(null))
                {
                    _context.Genre.Add(new Genre(genre.Name));
                    _context.SaveChanges();
                    _logger.Log(LogLevel.Information, genre.IdGenre.ToString());
                }
                _context.MovieDataGenre.Add(new MovieDataGenre(data.IdMovieData, genre.IdGenre));
            }
            foreach (Language language in movieData.Languages)
            {
                if (language.IdLanguage.Equals(null))
                {
                    _context.Language.Add(new Language(language.Name));
                    _context.SaveChanges();
                    _logger.Log(LogLevel.Information, language.IdLanguage.ToString());
                }
                _context.MovieDataLanguage.Add(new MovieDataLanguage(data.IdMovieData, language.IdLanguage));
            }

            _context.SaveChanges();
            var qGenres = _context.Genre.Join(_context.MovieDataGenre, g => g.IdGenre, mdg => mdg.IdGenre, (g, mdg) => new { g.IdGenre, g.Name, mdg.IdMovieData }).Where(g => g.IdMovieData == data.IdMovieData).ToList();
            List<Genre> genres = new List<Genre>();
            qGenres.ForEach(g => genres.Add(new Genre(g.IdGenre, g.Name)) );
            List<Language> languages = new List<Language>();
            var qLanguages = _context.Language.Join(_context.MovieDataGenre, g => g.IdLanguage, mdg => mdg.IdGenre, (g, mdg) => new { g.IdLanguage, g.Name, mdg.IdMovieData }).Where(g => g.IdMovieData == data.IdMovieData).ToList();
            qLanguages.ForEach(g => languages.Add(new Language(g.IdLanguage, g.Name)));
            Style[] styles = _context.Style.Where(s => s.IdStyle == data.IdStyle).ToArray<Style>();

            return data.MapToPresentationModel(movie.IdUser, genres.ToArray(), languages.ToArray(), image, styles);
        }

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            List<Movie> movies = this._context.Movie.ToList<Movie>();
            return movies;
        }

        [HttpGet("genres")]
        public IEnumerable<Genre> GetGenres()
        {
            return this._context.Genre.ToList<Genre>(); ;
        }

        [HttpGet("languages")]
        public IEnumerable<Language> GetLanguages()
        {
            return this._context.Language.ToList<Language>(); ;
        }

        [HttpGet("styles")]
        public IEnumerable<Style> GetStyles()
        {
            return this._context.Style.ToList<Style>(); ;
        }
    }
}