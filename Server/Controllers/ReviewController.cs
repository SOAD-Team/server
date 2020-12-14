using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Persistence;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewRepository reviewRepository;
        private readonly IMapper mapper;

        public ReviewController(ReviewRepository reviewRepository, IMapper mapper)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            var reviews = await reviewRepository.GetbyMovieId(id);
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(Review review)
        {
            Review insert = new Review { IdMovie = review.IdMovie, Score = review.Score, Comment = review.Comment};
            await reviewRepository.Create(insert);
            await reviewRepository.CompleteAsync();
            return Ok(review);
        }

    }
}
