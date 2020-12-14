using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence.Repositories
{
    public class ImageRepository : ImRepository<Image>, IImageRepository
    {
        public ImageRepository(ImagesDB context) : base(context) { }
        public async override Task<Image> Create(Image value)
        {
            var result = await _context.Create(value);
            return result;
        }

        public async override Task<Image> Get(string id)
        {
            var result = await _context.Get(id);
            return result;
        }

        public async override Task<IEnumerable<Image>> GetAll()
        {
            var result = await _context.Get();
            return result;
        }
    }
}
