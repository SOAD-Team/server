using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly GenreRepository genreRepository;

        public GenreController(GenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await genreRepository.GetAll();
            return Ok(genres);
        }
    }
}
