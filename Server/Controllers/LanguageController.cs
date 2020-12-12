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
        private readonly LanguageRepository languageRepository;
        public LanguageController(LanguageRepository languageRepository)
        {
            this.languageRepository = languageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguages()
        {
            return Ok(await languageRepository.GetAll());
        }
    }
}
