using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Persistence;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly LanguageRepository languageRepository;
        private readonly IMapper mapper;
        public LanguageController(LanguageRepository languageRepository, IMapper mapper)
        {
            this.languageRepository = languageRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguages()
        {
            var languages = await languageRepository.GetAll();
            return Ok(mapper.Map<IEnumerable<Resources.KeyValuePair>>(languages));
        }
    }
}
