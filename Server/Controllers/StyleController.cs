using Microsoft.AspNetCore.Mvc;
using Server.Persistence;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly StyleRepository styleRepository;
        public StyleController(StyleRepository styleRepository)
        {
            this.styleRepository = styleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetStyles()
        {
            return Ok(await styleRepository.GetAll());
        }
    }
}
