using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class MovieDataRepository : Repository<MovieData>, IMovieDataRepository
    {
        public MovieDataRepository(MoviesDB context) : base(context) { }
        public override async Task<MovieData> Create(MovieData data)
        {
            var result = await _context.MovieData.AddAsync(data);
            return result.Entity;
        }

        public async Task<MovieData> GetByMovieId(int id)
        {
            var result = await _context.MovieData.ToListAsync();
            MovieData latest = null;
            foreach (var movieData in result)
            {
                if(latest == null)
                {
                    latest = movieData;
                }
                else
                {
                    if(latest.RegisterDate < movieData.RegisterDate)
                    {
                        latest = movieData;
                    }
                }
            }
            return latest;
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
