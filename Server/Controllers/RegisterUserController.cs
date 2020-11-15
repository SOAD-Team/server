using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {

        private readonly MoviesDB _context;

        public RegisterUserController(MoviesDB context)
        {
            _context = context;
        }

        // POST api/<RegisterUserController>
        [HttpPost]
        public int RegisterUser(DTOs.User user)
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
        public DTOs.User LogIn(DTOs.UserData user)
        {
            User temp = _context.User.Where(usr => user.Email==usr.Email && user.Password==usr.Password ).FirstOrDefault();
            if (temp != null)
                return temp.MapToPresentationModel();
            else
                return null;
        }
    }
}
