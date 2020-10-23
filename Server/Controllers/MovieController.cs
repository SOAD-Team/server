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

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return this._context.Movies.ToList<Movie>();
        }
    }
}