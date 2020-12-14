using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public interface IReviewRepository
    {
        Task<Review> Create(Review value);
        Task<Review> Get(int id);
        Task<IEnumerable<Review>> GetAll();
        Task<IEnumerable<Review>> GetbyMovieId(int id);
    }
}