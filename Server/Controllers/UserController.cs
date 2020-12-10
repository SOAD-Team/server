using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Persistence;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MoviesDB _context;
        private readonly IMapper _mapper;

        public UserController(MoviesDB context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> RegisterUser(Resources.User user)
        {

            var user1 =  await _context.User.Where(usr => usr.Email == user.Email).Select(usr => usr.Email).FirstOrDefaultAsync();
            if(user1 == null)
            {
                User temp = new User(user.Email, user.Password, user.Name, user.LastName);
                _context.User.Add(temp);
                _context.SaveChanges();
                return Ok(_mapper.Map<Resources.User>(temp));
            }
            return NotFound(user);

        }

        [HttpPost]
        public async Task<IActionResult> LogIn(Resources.User user)
        {
            User temp = await _context.User.Where(usr => user.Email==usr.Email && user.Password==usr.Password ).FirstOrDefaultAsync();
            if (temp != null)
                return Ok(_mapper.Map<Resources.User>(temp));
            else
                return NotFound(user);
        }
    }
}
