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
            var userDatas = await _context.MovieData
                .Include(md => md.MovieDataLanguage)
                .Include(md => md.MovieDataGenre)
                .Join(_context.Movie,md => md.IdMovie, m => m.IdMovie,
                (md, m) => new { md, m }).Where(v => v.m.IdUser == id)
                .Select(val => val.md)
                .ToListAsync();

            var movies = new List<Models.MovieData>();

            foreach (var data in userDatas)
            {
                System.Console.WriteLine(data.Title);
                var existingMovie = movies.Where(m => m.IdMovie == data.IdMovie).FirstOrDefault();
                System.Console.WriteLine("Exists: " + (existingMovie != null));
                if (existingMovie == null)
                    movies.Add(data);
                else 
                    if (data.RegisterDate > existingMovie.RegisterDate)
                        movies[movies.IndexOf(existingMovie)] = data;
            }
            // userDatas = MovieControllerHelper.GetMostRecentData(userDatas, _context);
            // userDatas = MovieControllerHelper.FilterMovieDataByUser(userDatas, _context, id);

            var images = _mongoContext.Get().ToArray();
            images = MovieControllerHelper.FilterImages(images, movies.ToArray());

            var completeData = MovieControllerHelper.CreateData(movies.ToArray(), _context, _mapper);

            return Ok(MovieControllerHelper.CreateMovieDatas(completeData, images, id));
        }
    }
}
