using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public interface ILanguageRepository
    {
        Task<Language> Create(Language value);
        Task<Language> Get(int id);
        Task<IEnumerable<Language>> GetAll();
    }
}