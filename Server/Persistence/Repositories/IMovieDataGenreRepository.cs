using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public interface IMovieDataGenreRepository
    {
        Task<MovieDataGenre> Create(MovieDataGenre value);
        Task<MovieDataGenre> Get(int id);
        Task<IEnumerable<MovieDataGenre>> GetAll();
    }
}