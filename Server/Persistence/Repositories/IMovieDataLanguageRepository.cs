using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public interface IMovieDataLanguageRepository
    {
        Task<MovieDataLanguage> Create(MovieDataLanguage value);
        Task<MovieDataLanguage> Get(int id);
        Task<IEnumerable<MovieDataLanguage>> GetAll();
    }
}