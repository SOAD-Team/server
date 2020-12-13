using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Create(Image value);
        Task<Image> Get(string id);
        Task<IEnumerable<Image>> GetAll();
    }
}