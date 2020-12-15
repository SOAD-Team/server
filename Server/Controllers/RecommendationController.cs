using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Helpers;
using Server.Persistence;
using AutoMapper;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ReviewRepository reviewRepository;
        private readonly MovieDataRepository movieDataRepository;
        private readonly MovieRepository movieRepository;
        private readonly MovieDataGenreRepository genreRepository;

        public RecommendationController(IMapper mapper, ReviewRepository reviewRepository, MovieDataRepository movieDataRepository, MovieRepository movieRepository, MovieDataGenreRepository genreRepository)
        {
            _mapper = mapper;
            this.reviewRepository = reviewRepository;
            this.movieDataRepository = movieDataRepository;
            this.movieRepository = movieRepository;
            this.genreRepository = genreRepository;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Resources.UserPoints points)
        {
            List<Resources.Recommendation> recommendations = new List<Resources.Recommendation>();
            (await genreRepository.GetAll()).Where(g => g.IdGenre == points.Genre.Id)
                .Join(movieDataRepository.GetAll().Result, g => g.IdMovieData, md => md.IdMovieData, (g, md) => md)
                .ToList().ForEach(movie =>
                {
                    Resources.Movie data = _mapper.Map<Resources.Movie>(movie);
                    int score = ScoreHelper.GetRecommendationScore(points, data, movieDataRepository, reviewRepository);
                    Resources.Recommendation temp = new Resources.Recommendation { Movie = data, Score = score };
                    if (temp != null)
                        recommendations.Add(temp);
                });
            return Ok(recommendations.OrderByDescending(val => val.Score).Take(10));
        }
    }
}
