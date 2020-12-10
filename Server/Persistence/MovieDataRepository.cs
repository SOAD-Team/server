using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class MovieDataRepository : Repository<MovieData>
    {
        public MovieDataRepository(MoviesDB context) : base(context) { }
        public override async Task<MovieData> Create(MovieData data)
        {
            var result = await _context.MovieData.AddAsync(data);
            return result.Entity;
        }

        public override Task<MovieData> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public override Task<IEnumerable<MovieData>> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
