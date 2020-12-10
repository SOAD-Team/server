using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly MoviesDB _context;

        public GenreController(MoviesDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _context.Genre.ToListAsync();
            return Ok(genres);
        }
    }
}
