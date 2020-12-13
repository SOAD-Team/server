using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public interface IMovieDataRepository
    {
        Task<MovieData> Create(MovieData data);
        Task<MovieData> Get(int id);
        Task<IEnumerable<MovieData>> GetAll();
    }
}