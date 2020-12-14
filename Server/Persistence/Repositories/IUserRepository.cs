using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public interface IUserRepository
    {
        Task<User> Create(User value);
        Task<User> Get(int id);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByEmail(string email);
    }
}