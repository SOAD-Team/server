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

        public RecommendationController(IMapper mapper, ReviewRepository reviewRepository, MovieDataRepository movieDataRepository, MovieRepository movieRepository)
        {
            _mapper = mapper;
            this.reviewRepository = reviewRepository;
            this.movieDataRepository = movieDataRepository;
            this.movieRepository = movieRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Post([FromBody] Resources.UserPoints value)
        {
            List<Movie> movies = (List<Movie>) await movieRepository.GetAll();
            List<Resources.Recommendation> recommendations = new List<Resources.Recommendation>();
            foreach (Movie movie in movies)
            {
                Resources.Recommendation temp = ScoreHelper.GetRecommendationData(value, movie.IdMovie, movieDataRepository, movieRepository, reviewRepository, _mapper);
                if (temp != null)
                    foreach(Resources.KeyValuePair genre in temp.Movie.Genres)
                        if(genre.Id == value.Genre.IdGenre)
                        {
                            recommendations.Add(temp);
                            break;
                        }
            }

            return Ok(recommendations.OrderByDescending(val => val.Score).Take(10).ToArray());
        }
    }
}
