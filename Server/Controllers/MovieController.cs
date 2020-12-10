using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System.Linq;
using Server.Helpers;
using Server.Structs;
using Server.Persistence;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly MoviesDB _context;
        private readonly IImagesDB _mongoContext;
        private readonly IMapper _mapper;

        public MovieController(MoviesDB context, IImagesDB mongoContext, IMapper mapper)
        {
            _context = context;
            _mongoContext = mongoContext;
            _mapper = mapper;
        }
        // Create
        [HttpPost]
        public async Task<IActionResult> CreateMovie(Resources.Movie movieData)
        {
            Movie movie = _mapper.Map<Movie>(movieData);
            await _context.Movie.AddAsync(movie);
            _context.SaveChanges();
            int movieId = movie.IdMovie;

            return Ok(MovieControllerHelper.CreateMovieDataOnDb(_context, movieId, movieData, _mapper));

        }
        // Get All
        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = new List<Models.MovieData>();

            await _context.MovieData
                .Include(md => md.MovieDataLanguage)
                .Include(md => md.MovieDataGenre)
                .Select(val => val).ForEachAsync(data =>
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

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetMovieByUserId(int id)
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

        // Get
        [HttpGet("{id}")]
        public Resources.Movie GetMovie(int id)
        {
            Movie userMovies = _context.Movie.Where(m => m.IdMovie == id).FirstOrDefault();
            MovieData[] userDatas = _context.MovieData.ToArray();
            userDatas = MovieControllerHelper.FilterMovieDataByMovie(userDatas, id).ToArray();
            userDatas = MovieControllerHelper.GetMostRecentData(userDatas, _context).ToArray();

            Image[] images = _mongoContext.Get().ToArray();
            images = MovieControllerHelper.FilterImages(images, userDatas);

            Data[] completeData = MovieControllerHelper.CreateData(userDatas, _context, _mapper);

            foreach (var mdata in completeData)
                System.Console.WriteLine(mdata.MData.Title);

            Resources.Movie data = MovieControllerHelper.CreateMovieDatas(completeData, images, userMovies.IdUser).Last();

            return data;
        }
        // Put
        [HttpPut]
        public Resources.Movie UpdateMovie(Resources.Movie movieData)
        {
            int movieId = movieData.IdMovie.Value;
            return MovieControllerHelper.CreateMovieDataOnDb(_context, movieId, movieData, _mongoContext, _mapper);
        }

        // Delete
        [HttpGet("score/{id}")]
        public int GetMovieCommunityScore(int id)
        {
            return RecommendationHelper.GetMovieCommunityScore(id, _context);
        }

        [HttpGet("popularity/{id}")]
        public int GetMoviePopularity(int id)
        {
            return RecommendationHelper.GetMoviePopularity(id, _context);
        }


    }
}