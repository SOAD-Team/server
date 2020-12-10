using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly MoviesDB _context;

        public ReviewController(MoviesDB context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            var reviews = await _context.Review.Where(r => r.IdMovie == id).ToListAsync();
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(Review review)
        {
            Review insert = new Review { IdMovie = review.IdMovie, Score = review.Score, Comment = review.Comment};
            await _context.Review.AddAsync(insert);
            _context.SaveChanges();
            return Ok(review);
        }

    }
}
