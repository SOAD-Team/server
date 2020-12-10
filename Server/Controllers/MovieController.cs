using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System.Linq;
using Server.Helpers;
using Server.Structs;
using Server.Persistence;
using AutoMapper;

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
        public Resources.Movie CreateMovie(Resources.Movie movieData)
        {
            Movie movie = new Movie(movieData.IdUser);
            _context.Movie.Add(movie);
            _context.SaveChanges();
            int movieId = movie.IdMovie;
            return MovieControllerHelper.CreateMovieDataOnDb(_context, movieId, movieData, _mongoContext, _mapper);

        }
        // Get All
        [HttpGet]
        public IEnumerable<Movie> GetMovies()
        {
            List<Movie> movies = this._context.Movie.ToList<Movie>();
            return movies;
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