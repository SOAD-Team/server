using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(MoviesDB context) : base(context) { }

        public override Task<Genre> Create(Genre value)
        {
            throw new NotImplementedException();
        }

        public override Task<Genre> Get(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<Genre>> GetAll()
        {
            var result = await _context.Genre.ToListAsync();
            return result;
        }
    }
}
