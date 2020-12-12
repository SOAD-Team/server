using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class StyleRepository : Repository<Style>
    {
        public StyleRepository(MoviesDB context) : base(context) { }

        public override Task<Style> Create(Style value)
        {
            throw new System.NotImplementedException();
        }

        public override Task<Style> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<IEnumerable<Style>> GetAll()
        {
            var result = await _context.Style.ToListAsync();
            return result;
        }
    }
}
