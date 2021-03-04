using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public interface IMovieRepository
    {
        Task<Movie> Create(Movie value);
        Task<Movie> Get(int id);
        Task<IEnumerable<Movie>> GetAll();
    }
}