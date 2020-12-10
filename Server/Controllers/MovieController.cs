using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Server.Helpers;
using Server.Structs;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly MoviesDB _context;
        private readonly IImagesDB _mongoContext;

        public MovieController(MoviesDB context, IImagesDB mongoContext)
        {
            _context = context;
            _mongoContext = mongoContext;
        }

        [HttpPost("image")]
        public async Task<Resources.Image> CreateImage([FromForm] IFormFile image)
        {
            System.Console.WriteLine(image.Name);
            byte[] fileBytes;

            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                fileBytes = stream.ToArray();
            }

            Image data = new Image(fileBytes);
            data = _mongoContext.Create(data);

            return data.MapToPresentationModel();
        }

        [HttpPost]
        public Resources.Movie CreateMovie(Resources.Movie movieData)
        {
            Movie movie = new Movie(movieData.IdUser);
            _context.Movie.Add(movie);
            _context.SaveChanges();
            int movieId = movie.IdMovie;
            return MovieControllerHelper.CreateMovieDataOnDb(_context, movieId, movieData, _mongoContext);

        }
        [HttpGet("user/{id}")]
        public IEnumerable<Resources.Movie> GetMovieDataByUserId(int id)
        {
            List<MovieData> userDatas = _context.MovieData.ToList();
            userDatas =  MovieControllerHelper.GetMostRecentData(userDatas, _context);
            userDatas = MovieControllerHelper.FilterMovieDataByUser(userDatas, _context, id);

            Image[] images = _mongoContext.Get().ToArray();
            images = MovieControllerHelper.FilterImages(images, userDatas.ToArray());

            Data[] completeData = MovieControllerHelper.CreateData(userDatas.ToArray(), _context);

            return MovieControllerHelper.CreateMovieDatas(completeData, images, id);
        }

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

        [HttpGet]
        public IEnumerable<Movie> GetMovies()
        {
            List<Movie> movies = this._context.Movie.ToList<Movie>();
            return movies;
        }

        [HttpGet("moviedata")]
        public IEnumerable<MovieData> GetMovieData()
        {
            List<MovieData> movies = this._context.MovieData.ToList<MovieData>();

            List<MovieData> filtred = MovieControllerHelper.GetMostRecentData(movies, this._context);

            return filtred;
        }

        [HttpGet("genres")]
        public IEnumerable<Genre> GetGenres()
        {
            return this._context.Genre.ToList<Genre>();
        }

        [HttpGet("languages")]
        public IEnumerable<Language> GetLanguages()
        {
            return this._context.Language.ToList<Language>();
        }

        [HttpGet("styles")]
        public IEnumerable<Style> GetStyles()
        {
            return this._context.Style.ToList<Style>();
        }

        [HttpGet("movieimages/{id}")]
        public IActionResult GetMovieImages(string id)
        {
            byte[] images = _mongoContext.Get(id).ObjectImage;
            return File(images, "image/jpeg");
        }

        [HttpGet("moviebyid/{id}")]
        public Resources.Movie GetMovieById(int id)
        {
            Movie userMovies = _context.Movie.Where(m => m.IdMovie == id).FirstOrDefault();
            MovieData[] userDatas = _context.MovieData.ToArray();
            userDatas = MovieControllerHelper.FilterMovieDataByMovie(userDatas, id).ToArray();
            userDatas = MovieControllerHelper.GetMostRecentData(userDatas, _context).ToArray();

            Image[] images = _mongoContext.Get().ToArray();
            images = MovieControllerHelper.FilterImages(images, userDatas);

            Data[] completeData = MovieControllerHelper.CreateData(userDatas, _context);

            foreach (var mdata in completeData)
                System.Console.WriteLine(mdata.MData.Title);

            Resources.Movie data = MovieControllerHelper.CreateMovieDatas(completeData, images, userMovies.IdUser).Last();

            return data;
        }

        [HttpPost("update")]
        public Resources.Movie UpdateMovie(Resources.Movie movieData)
        {
            int movieId = movieData.IdMovie.Value;
            return MovieControllerHelper.CreateMovieDataOnDb(_context, movieId, movieData, _mongoContext);
        }


        [HttpPost("review")]
        public Review CreateReview(Review review)
        {
            Review insert = new Review();
            insert.IdMovie = review.IdMovie;
            insert.Score = review.Score;
            insert.Comment = review.Comment;
            _context.Review.Add(insert);
            _context.SaveChanges();
            return review;

        }

        [HttpGet("review/{id}")]
        public IEnumerable<Review> GetReview(int id)
        {
            List<Review> reviews = _context.Review.Where(r => r.IdMovie == id).ToList();

            return reviews;
        }
    }
}