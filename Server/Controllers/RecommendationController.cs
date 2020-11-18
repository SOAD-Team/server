using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly MoviesDB _context;
        private readonly IImagesDB _mongoContext;

        public RecommendationController(MoviesDB context, IImagesDB mongoContext)
        {
            _context = context;
            _mongoContext = mongoContext;
        }
        [HttpPost]
        public IEnumerable<DTOs.Recommendation> Post([FromBody] DTOs.UserPoints value)
        {
            Movie[] movies = _context.Movie.ToArray();
            List<DTOs.Recommendation> recommendations = new List<DTOs.Recommendation>();
            foreach (Movie movie in movies)
            {
                DTOs.Recommendation temp = RecommendationHelper.GetRecommendationData(value, movie.IdMovie, _context, _mongoContext);
                if (temp != null)
                    foreach(Genre genre in temp.Movie.Genres)
                        if(genre.IdGenre == value.Genre.IdGenre)
                        {
                            recommendations.Add(temp);
                            break;
                        }
            }

            return RecommendationHelper.FilterRecommendations(recommendations.ToArray());
        }
    }
}
