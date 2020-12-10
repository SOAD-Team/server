using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Helpers;
using Server.Persistence;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly MoviesDB _context;
        private readonly IImagesDB _mongoContext;
        private readonly IMapper _mapper;

        public RecommendationController(MoviesDB context, IImagesDB mongoContext, IMapper mapper)
        {
            _context = context;
            _mongoContext = mongoContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Post([FromBody] Resources.UserPoints value)
        {
            Movie[] movies = await _context.Movie.ToArrayAsync();
            List<Resources.Recommendation> recommendations = new List<Resources.Recommendation>();
            foreach (Movie movie in movies)
            {
                Resources.Recommendation temp = RecommendationHelper.GetRecommendationData(value, movie.IdMovie, _context, _mapper);
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
