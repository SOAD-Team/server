using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Persistence;
using AutoMapper;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MovieRepository movieRepository;
        private readonly MovieDataRepository movieDataRepository;
        private readonly MovieDataGenreRepository genreRepository;
        private readonly MovieDataLanguageRepository languageRepository;

        public MovieController(IMapper mapper, MovieRepository movieRepository, MovieDataRepository movieDataRepository, MovieDataGenreRepository genreRepository ,MovieDataLanguageRepository languageRepository)
        {
            _mapper = mapper;
            this.movieRepository = movieRepository;
            this.movieDataRepository = movieDataRepository;
            this.genreRepository = genreRepository;
            this.languageRepository = languageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(Resources.Movie movieData)
        {
            Movie movie = _mapper.Map<Movie>(movieData);
            await movieRepository.Create(movie);
            await movieRepository.CompleteAsync();
            movieData.IdMovie = movie.IdMovie;
            MovieData data = _mapper.Map<MovieData>(movieData);
            data = await movieDataRepository.Create(data);
            await movieDataRepository.CompleteAsync();
            foreach (var genre in movieData.Genres)
                await genreRepository.Create(new MovieDataGenre(data.IdMovieData, genre.Id));
            foreach (var language in movieData.Languages)
                await languageRepository.Create(new MovieDataLanguage(data.IdMovieData, language.Id));
            await movieDataRepository.CompleteAsync();

            return Ok(_mapper.Map<Resources.Movie>(data));

        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await movieDataRepository.GetAll();

            var resourceMovies = _mapper.Map<IEnumerable<Resources.Movie>>(movies);

            return Ok(resourceMovies);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetMovieByUserId(int id)
        {
            var movies = await movieDataRepository.GetByUserId(id);

            var resourceMovies = _mapper.Map<IEnumerable<Resources.Movie>>(movies);

            return Ok(resourceMovies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            MovieData movie = await movieDataRepository.Get(id);
            return Ok(_mapper.Map<Resources.Movie>(movie));
        }

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

            return Ok(_mapper.Map<Resources.Movie>(data));
        }

    }
}