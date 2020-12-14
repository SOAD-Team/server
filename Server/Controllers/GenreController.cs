using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly GenreRepository genreRepository;
        private readonly IMapper mapper;

        public GenreController(GenreRepository genreRepository, IMapper mapper)
        {
            this.genreRepository = genreRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await genreRepository.GetAll();
            return Ok(mapper.Map<IEnumerable<Resources.KeyValuePair>>(genres));
        }
    }
}
