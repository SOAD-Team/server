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
        private readonly IMovieRepository movieRepository;
        private readonly IMovieDataRepository movieDataRepository;
        private readonly IMovieDataGenreRepository genreRepository;
        private readonly IMovieDataLanguageRepository languageRepository;
        private readonly IUnitOfWork unitOfWork;

        public MovieController(IMapper mapper, IMovieRepository movieRepository, IMovieDataRepository movieDataRepository, IMovieDataGenreRepository genreRepository , IMovieDataLanguageRepository languageRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            this.movieRepository = movieRepository;
            this.movieDataRepository = movieDataRepository;
            this.genreRepository = genreRepository;
            this.languageRepository = languageRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Resources.Movie movieData)
        {
            Movie movie = _mapper.Map<Movie>(movieData);
            await movieRepository.Create(movie);
            await unitOfWork.CompleteAsync();
            movieData.IdMovie = movie.IdMovie;
            MovieData data = _mapper.Map<MovieData>(movieData);
            data = await movieDataRepository.Create(data);
            await unitOfWork.CompleteAsync();
            foreach (var genre in movieData.Genres)
                await genreRepository.Create(new MovieDataGenre(data.IdMovieData, genre.Id));
            foreach (var language in movieData.Languages)
                await languageRepository.Create(new MovieDataLanguage(data.IdMovieData, language.Id));
            await unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<Resources.Movie>(data));

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
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
        public async Task<IActionResult> Get(int id)
        {
            Movie movie = await movieRepository.Get(id);
            Resources.Movie result = _mapper.Map<Resources.Movie>(movie);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Resources.Movie movieData)
        {
            MovieData data = _mapper.Map<MovieData>(movieData);
            data = await movieDataRepository.Create(data);
            await unitOfWork.CompleteAsync();
            foreach (var genre in movieData.Genres)
                await genreRepository.Create(new MovieDataGenre(data.IdMovieData, genre.Id));
            foreach (var language in movieData.Languages)
                await languageRepository.Create(new MovieDataLanguage(data.IdMovieData, language.Id));
            await unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<Resources.Movie>(data));
        }

    }
}