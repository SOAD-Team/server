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
        public async Task<DTOs.Image> CreateImage([FromForm] IFormFile image)
        {
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
        public DTOs.MovieData CreateMovie(DTOs.MovieData movieData)
        {
            Movie movie = new Movie(movieData.IdUser);
            _context.Movie.Add(movie);
            _context.SaveChanges();
            int movieId = movie.IdMovie;
            MovieData data = movieData.MapToModel(movieId, movieData.Image.Id);
            _context.MovieData.Add(data);
            _context.SaveChanges();

            foreach (Genre genre in movieData.Genres)
            {
                if (genre.IdGenre.Equals(null))
                {
                    _context.Genre.Add(new Genre(genre.Name));
                    _context.SaveChanges();
                }
                _context.MovieDataGenre.Add(new MovieDataGenre(data.IdMovieData, genre.IdGenre));
            }
            foreach (Language language in movieData.Languages)
            {
                if (language.IdLanguage.Equals(null))
                {
                    _context.Language.Add(new Language(language.Name));
                    _context.SaveChanges();
                }
                _context.MovieDataLanguage.Add(new MovieDataLanguage(data.IdMovieData, language.IdLanguage));
            }

            _context.SaveChanges();
            var qGenres = _context.Genre.Join(_context.MovieDataGenre, g => g.IdGenre, mdg => mdg.IdGenre, (g, mdg) => new { g.IdGenre, g.Name, mdg.IdMovieData }).Where(g => g.IdMovieData == data.IdMovieData).ToList();
            List<Genre> genres = new List<Genre>();
            qGenres.ForEach(g => genres.Add(new Genre(g.IdGenre, g.Name)) );
            List<Language> languages = new List<Language>();
            var qLanguages = _context.Language.Join(_context.MovieDataLanguage, g => g.IdLanguage, mdg => mdg.IdLanguage, (g, mdg) => new { g.IdLanguage, g.Name, mdg.IdMovieData }).Where(g => g.IdMovieData == data.IdMovieData).ToList();
            qLanguages.ForEach(g => languages.Add(new Language(g.IdLanguage, g.Name)));
            Style[] styles = _context.Style.Where(s => s.IdStyle == data.IdStyle).ToArray<Style>();

            return data.MapToPresentationModel(movie.IdUser, genres.ToArray(), languages.ToArray(), _mongoContext, styles);
        }
        [HttpGet("user/{id}")]
        public IEnumerable<DTOs.MovieData> GetMovieDataByUserId(int id)
        {
            Movie[] userMovies = _context.Movie.Where(m => m.IdUser == id).ToArray();
            List<MovieData> userDatas = _context.MovieData.ToList();
            userDatas =  MovieControllerHelper.FilterMovieData(userDatas, _context);
            Image[] images = _mongoContext.Get().ToArray();
            images = MovieControllerHelper.FilterImages(images, userDatas.ToArray());

            Data[] completeData = MovieControllerHelper.CreateData(userDatas.ToArray(), _context);

            return MovieControllerHelper.CreateMovieDatas(completeData, images, id);
        }

        [HttpGet("score/{id}")]
        public int GetMovieScore(int id)
        {
            return 0;
        }

        [HttpGet("popularity/{id}")]
        public int GetMoviePopularity(int id)
        {
            return 0;
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

            List<MovieData> filtred = MovieControllerHelper.FilterMovieData(movies, this._context);

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
        public DTOs.MovieData GetMovieById(int id)
        {
            Movie userMovies = _context.Movie.Where(m => m.IdMovie == id).FirstOrDefault();
            MovieData[] userDatas = _context.MovieData.ToArray();
            Image[] images = _mongoContext.Get().ToArray();
            images = MovieControllerHelper.FilterImages(images, userDatas);

            Data[] completeData = MovieControllerHelper.CreateData(userDatas, _context);


            DTOs.MovieData data = MovieControllerHelper.CreateMovieDatas(completeData, images, userMovies.IdUser).Last();

            return data;
        }

        [HttpPost("update")]
        public DTOs.MovieData UpdateMovie(DTOs.MovieData movieData)
        {
            int movieId = movieData.IdMovie.Value;
            Movie movie = _context.Movie.Find(movieId);
            MovieData data = movieData.MapToModel(movieId, movieData.Image.Id);
            _context.MovieData.Add(data);
            _context.SaveChanges();

            foreach (Genre genre in movieData.Genres)
            {
                if (genre.IdGenre.Equals(null))
                {
                    _context.Genre.Add(new Genre(genre.Name));
                    _context.SaveChanges();
                }
                _context.MovieDataGenre.Add(new MovieDataGenre(data.IdMovieData, genre.IdGenre));
            }
            foreach (Language language in movieData.Languages)
            {
                if (language.IdLanguage.Equals(null))
                {
                    _context.Language.Add(new Language(language.Name));
                    _context.SaveChanges();
                }
                _context.MovieDataLanguage.Add(new MovieDataLanguage(data.IdMovieData, language.IdLanguage));
            }

            _context.SaveChanges();
            var qGenres = _context.Genre.Join(_context.MovieDataGenre, g => g.IdGenre, mdg => mdg.IdGenre, (g, mdg) => new { g.IdGenre, g.Name, mdg.IdMovieData }).Where(g => g.IdMovieData == data.IdMovieData).ToList();
            List<Genre> genres = new List<Genre>();
            qGenres.ForEach(g => genres.Add(new Genre(g.IdGenre, g.Name)));
            List<Language> languages = new List<Language>();
            var qLanguages = _context.Language.Join(_context.MovieDataLanguage, g => g.IdLanguage, mdg => mdg.IdLanguage, (g, mdg) => new { g.IdLanguage, g.Name, mdg.IdMovieData }).Where(g => g.IdMovieData == data.IdMovieData).ToList();
            qLanguages.ForEach(g => languages.Add(new Language(g.IdLanguage, g.Name)));
            Style[] styles = _context.Style.Where(s => s.IdStyle == data.IdStyle).ToArray<Style>();

            return data.MapToPresentationModel(movie.IdUser, genres.ToArray(), languages.ToArray(), _mongoContext, styles);
        }
    }
}