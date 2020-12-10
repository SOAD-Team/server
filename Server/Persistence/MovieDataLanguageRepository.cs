using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class MovieDataLanguageRepository : Repository<MovieDataLanguage>
    {
        public MovieDataLanguageRepository(MoviesDB context) : base(context)
        {
        }

        public override async Task<MovieDataLanguage> Create(MovieDataLanguage value)
        {
            var result = await _context.MovieDataLanguage.AddAsync(value);
            return result.Entity;
        }

        public override Task<MovieDataLanguage> Get(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<MovieDataLanguage>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
