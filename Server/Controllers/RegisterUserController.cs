﻿using System;
using System.Collections.Generic;
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
                User temp = new User();
                temp.LastName = user.LastName;
                temp.Name = user.Name;
                temp.Email = user.Email;
                temp.Password = user.Password;
                temp.IsActive = true;
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
    }
}
