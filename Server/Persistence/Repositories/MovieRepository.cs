using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MoviesDB context) : base(context)
        {

        }

        public override async Task<Movie> Create(Movie value)
        {
            var result = await _context.Movie.AddAsync(value);
            value = result.Entity;
            return value;
        }

        public async override Task<Movie> Get(int id)
        {
            var result = await _context.Movie.Where(val => val.IdMovie == id).FirstOrDefaultAsync();
            return result;
        }

        public override Task<IEnumerable<Movie>> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
