using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public abstract class ImRepository<T>
    {
        protected readonly ImagesDB _context;

        protected ImRepository(ImagesDB context)
        {
            _context = context;
        }

        public abstract Task<T> Create(T value);
        public abstract Task<T> Get(string id);
        public abstract Task<IEnumerable<T>> GetAll();
        
    }
}
