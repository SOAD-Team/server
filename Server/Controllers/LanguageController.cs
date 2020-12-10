using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Persistence;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly MoviesDB _context;
        public LanguageController(MoviesDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguages()
        {
            return Ok(await _context.Language.ToListAsync());
        }
    }
}
