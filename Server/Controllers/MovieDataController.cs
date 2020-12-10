using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Helpers;
using Server.Persistence;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieDataController : ControllerBase
    {
        private readonly MoviesDB _context;
        private readonly IImagesDB _mongoContext;
        private readonly IMapper _mapper;

        public MovieDataController(MoviesDB context, IImagesDB mongoContext, IMapper mapper)
        {
            _context = context;
            _mongoContext = mongoContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetMovieData()
        {
            var movies = await this._context.MovieData.ToListAsync();
            var filtred = MovieControllerHelper.GetMostRecentData(movies, _context);

            return Ok(filtred);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieDataByUserId(int id)
        {
            var movies = new List<Models.MovieData>();

            await _context.MovieData
                .Include(md => md.MovieDataLanguage)
                .Include(md => md.MovieDataGenre)
                .Join(_context.Movie, md => md.IdMovie, m => m.IdMovie,
                (md, m) => new { md, m }).Where(v => v.m.IdUser == id)
                .Select(val => val.md).ForEachAsync(data =>
                {
                    var existingMovie = movies.Where(m => m.IdMovie == data.IdMovie).FirstOrDefault();
                    if (existingMovie == null)
                        movies.Add(data);
                    else
                        if (data.RegisterDate > existingMovie.RegisterDate)
                        movies[movies.IndexOf(existingMovie)] = data;
                });

            var resourceMovies = _mapper.Map<IEnumerable<Resources.Movie>>(movies);
            return Ok(resourceMovies);
        }
    }
}
