using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly UserRepository userRepository;

        public UserController(IMapper mapper, UserRepository userRepository)
        {
            this._mapper = mapper;
            this.userRepository = userRepository;
        }

        [HttpPut]
        public async Task<IActionResult> RegisterUser(Resources.User user)
        {
            var user1 = await userRepository.GetByEmail(user.Email);
            if(user1 == null)
            {
                User temp = new User(user.Email, user.Password, user.Name, user.LastName);
                await userRepository.Create(temp);
                await userRepository.CompleteAsync();
                return Ok(_mapper.Map<Resources.User>(temp));
            }
            return NotFound(user);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(Resources.User user)
        {
            User temp = await userRepository.GetByEmail(user.Email);
            if (temp != null)
                if (temp.Password.Equals(user.Password))
                    return Ok(_mapper.Map<Resources.User>(temp));
                else 
                    return NotFound(user);
            else
                return NotFound(user);
        }
    }
}
