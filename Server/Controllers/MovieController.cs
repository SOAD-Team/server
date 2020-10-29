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
            Console.WriteLine(movieData);
            // string imageId = _mongoContext.Create(movieData.Image.MapToImage()).Id;
            return null;
        }

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            this._logger.Log(LogLevel.Information,"API is Working & Connecting to DB");
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