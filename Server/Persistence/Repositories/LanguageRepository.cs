using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        public LanguageRepository(MoviesDB context) : base(context)
        {

        }

        public async override Task<Language> Create(Language value)
        {
            var lang = await _context.Language.AddAsync(value);
            return lang.Entity;
        }

        public override async Task<Language> Get(int id)
        {
            var result = await _context.Language.Where(g => g.IdLanguage == id).FirstOrDefaultAsync();
            return result;
        }

        public override async Task<IEnumerable<Language>> GetAll()
        {
            var result = await _context.Language.ToListAsync();
            return result;
        }
    }
}
