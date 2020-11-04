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

        private readonly MoviesDB _context;

        public RegisterUserController(MoviesDB context)
        {
            _context = context;
        }

        // POST api/<RegisterUserController>
        [HttpPost]
        public int RegisterUser(DTOs.User user)
        {
            int status_code = 0;
            try
            {
                User temp = new User();
                temp.LastName = user.LastName;
                temp.Name = user.Name;
                temp.Email = user.Email;
                temp.Password = user.Password;
                temp.IsActive = true;
                _context.User.Add(temp);
                _context.SaveChanges();
                status_code = 1;

            }
            catch (Exception)
            {
                status_code = 0;
            }
            return status_code;
        }
    }
}
