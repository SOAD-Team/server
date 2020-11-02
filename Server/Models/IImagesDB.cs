using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public interface IImagesDB
    {
        public List<Image> Get();

        public Image Get(string id);

        public Image Create(Image Image);

        public void Update(string id, Image ImageIn);

        public void Remove(Image ImageIn);

        public void Remove(string id);
    }
}
