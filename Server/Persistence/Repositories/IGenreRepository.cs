using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public interface IGenreRepository
    {
        Task<Genre> Create(Genre value);
        Task<Genre> Get(int id);
        Task<IEnumerable<Genre>> GetAll();
    }
}