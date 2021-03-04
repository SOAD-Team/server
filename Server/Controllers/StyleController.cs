using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly IStyleRepository styleRepository;
        private readonly IMapper mapper;
        public StyleController(IStyleRepository styleRepository, IMapper mapper)
        {
            this.styleRepository = styleRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStyles()
        {
            var styles = await styleRepository.GetAll();
            return Ok(mapper.Map<IEnumerable<Resources.KeyValuePair>>(styles));
        }
    }
}
