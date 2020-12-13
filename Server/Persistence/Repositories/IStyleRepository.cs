using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public interface IStyleRepository
    {
        Task<Style> Create(Style value);
        Task<Style> Get(int id);
        Task<IEnumerable<Style>> GetAll();
    }
}