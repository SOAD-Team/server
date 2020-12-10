using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(MoviesDB context)
        {
            _context = context;
        }

        // POST api/<UserController>
        [HttpPost]
        public int RegisterUser(Resources.User user)
        {
            int statusCode = 0;

            var user1 =  _context.User.Where(usr => usr.Email == user.Email).Select(usr => usr.Email).FirstOrDefault();
            if(user1 == null)
            {
                User temp = new User(user.Email, user.Password, user.Name, user.LastName);
                _context.User.Add(temp);
                _context.SaveChanges();
                statusCode = 1;
            }
            else
            {
                statusCode = 0;
            }

            return statusCode;

        }

        // POST api/<LogInUserController>
        [HttpPost("login")]
        public Resources.User LogIn(Resources.User user)
        {
            User temp = _context.User.Where(usr => user.Email==usr.Email && user.Password==usr.Password ).FirstOrDefault();
            if (temp != null)
                return temp.MapToPresentationModel();
            else
                return null;
        }
    }
}
