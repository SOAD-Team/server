using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Persistence 
{ 
    public interface IImagesDB
    {
        public Task<List<Image>> Get();

        public Task<Image> Get(string id);

        public Task<Image> Create(Image Image);

        public void Update(string id, Image ImageIn);

        public void Remove(Image ImageIn);

        public void Remove(string id);
    }
}
