using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(MoviesDB context) : base(context) { }

        public override async Task<User> Create(User value)
        {
            var result = await _context.User.AddAsync(value);
            return result.Entity;
        }

        public override async Task<User> Get(int id)
        {
            var result = await _context.User.FindAsync(id);
            return result;
        }

        public async Task<User> GetByEmail(string email)
        {
            var result = await _context.User.Where(usr => usr.Email == email).FirstOrDefaultAsync();
            return result;
        }

        public override Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
