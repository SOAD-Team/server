using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {

        private readonly ILogger<MovieController> _logger;
        private readonly MoviesDB _context;
        private readonly ImagesDB _mongoContext;

        public RegisterUserController(ILogger<MovieController> logger, MoviesDB context, ImagesDB mongoContext)
        {
            _logger = logger;
            _context = context;
            _mongoContext = mongoContext;
        }

        // GET: api/<RegisterUserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RegisterUserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RegisterUserController>
        [HttpPost]
        public Models.User Post(Models.User user)
        {
            //_context.User.Add(user);
            //_context.SaveChanges();
            return user;
        }

        // PUT api/<RegisterUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RegisterUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
