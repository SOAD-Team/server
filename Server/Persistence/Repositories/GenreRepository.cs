using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(MoviesDB context) : base(context) { }

        public async override Task<Genre> Create(Genre value)
        {
            var genre = await _context.AddAsync(value);
            return genre.Entity;
        }

        public async override Task<Genre> Get(int id)
        {
            var result = await _context.Genre.Where(g => g.IdGenre == id).FirstOrDefaultAsync();
            return result;
        }

        public override async Task<IEnumerable<Genre>> GetAll()
        {
            var result = await _context.Genre.ToListAsync();
            return result;
        }
    }
}
