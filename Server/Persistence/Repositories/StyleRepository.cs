using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class StyleRepository : Repository<Style>, IStyleRepository
    {
        public StyleRepository(MoviesDB context) : base(context) { }

        public async override Task<Style> Create(Style value)
        {
            var result = await _context.Style.AddAsync(value);
            return result.Entity;
        }

        public override async Task<Style> Get(int id)
        {
            var result = await _context.Style.Where(g => g.IdStyle == id).FirstOrDefaultAsync();
            return result;
        }

        public override async Task<IEnumerable<Style>> GetAll()
        {
            var result = await _context.Style.ToListAsync();
            return result;
        }
    }
}
