using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(MoviesDB context) : base(context) { }

        public override async Task<Review> Create(Review value)
        {
            var result = await _context.Review.AddAsync(value);
            return result.Entity;
        }

        public override Task<Review> Get(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<Review>> GetAll()
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Review>> GetbyMovieId(int id)
        {
            var result = await _context.Review.Where(r => r.IdMovie == id).ToListAsync();
            return result;

        }
    }
}
