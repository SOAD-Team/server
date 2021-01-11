using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;


        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.reviewRepository = reviewRepository;
            this._mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            var reviews = await reviewRepository.GetbyMovieId(id);
            return Ok(_mapper.Map<IEnumerable<Resources.Review>>(reviews));
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(Resources.Review review)
        {
            Review insert = new Review { IdMovie = review.IdMovie, Score = review.Score, Comment = review.Comment};
            await reviewRepository.Create(insert);
            await unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<Resources.Review>(review));
        }

    }
}
