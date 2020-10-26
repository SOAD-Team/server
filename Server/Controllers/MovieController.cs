using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Models;
using System.Linq;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly MoviesDB _context;

        public MovieController(ILogger<MovieController> logger, MoviesDB context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            List<Movie> movies = this._context.Movie.ToList<Movie>();
            return movies;
        }
    }
}