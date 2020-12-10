using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Persistence;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly MoviesDB _context;
        public StyleController(MoviesDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStyles()
        {
            return Ok(await _context.Style.ToListAsync());
        }
    }
}
