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
        private readonly IMapper _mapper;
        private readonly MovieRepository movieRepository;
        private readonly MovieDataRepository movieDataRepository;
        private readonly MovieDataGenreRepository genreRepository;
        private readonly MovieDataLanguageRepository languageRepository;

        public MovieController(MoviesDB context, IMapper mapper, MovieRepository movieRepository, MovieDataRepository movieDataRepository, MovieDataGenreRepository genreRepository ,MovieDataLanguageRepository languageRepository)
        {
            _context = context;
            _mapper = mapper;
            this.movieRepository = movieRepository;
            this.movieDataRepository = movieDataRepository;
            this.genreRepository = genreRepository;
            this.languageRepository = languageRepository;
        }
        // Create
        [HttpPost]
        public async Task<IActionResult> CreateMovie(Resources.Movie movieData)
        {
            Movie movie = _mapper.Map<Movie>(movieData);
            await movieRepository.Create(movie);
            await movieRepository.CompleteAsync();
            MovieData data = _mapper.Map<MovieData>(movieData);
            data = await movieDataRepository.Create(data);
            await movieDataRepository.CompleteAsync();
            foreach (var genre in movieData.Genres)
                await genreRepository.Create(new MovieDataGenre(data.IdMovieData, genre.Id));
            foreach (var language in movieData.Languages)
                await languageRepository.Create(new MovieDataLanguage(data.IdMovieData, language.Id));
            await movieDataRepository.CompleteAsync();

            return Ok(_mapper.Map<Resources.Movie>(movieData));

        }
        // Get All
        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = new List<MovieData>();

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
            var movies = new List<MovieData>();

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
        public async Task<IActionResult> GetMovie(int id)
        {
            MovieData movie = await _context.MovieData.Where(m => m.IdMovieData == id).FirstOrDefaultAsync();
            return Ok(_mapper.Map<Resources.Movie>(movie));
        }
        // Put
        [HttpPut]
        public async Task<IActionResult> UpdateMovie(Resources.Movie movieData)
        {
            MovieData data = _mapper.Map<MovieData>(movieData);
            data = await movieDataRepository.Create(data);
            await movieDataRepository.CompleteAsync();
            foreach (var genre in movieData.Genres)
                await genreRepository.Create(new MovieDataGenre(data.IdMovieData, genre.Id));
            foreach (var language in movieData.Languages)
                await languageRepository.Create(new MovieDataLanguage(data.IdMovieData, language.Id));
            await movieDataRepository.CompleteAsync();

            await movieDataRepository.CompleteAsync();

            return Ok(_mapper.Map<Resources.Movie>(movieData));
        }

    }
}