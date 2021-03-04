using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Persistence
{
    public abstract class Repository<T>
    {
        protected readonly MoviesDB _context;

        protected Repository(MoviesDB context)
        {
            _context = context;
        }

        public abstract Task<T> Create(T value);
        public abstract Task<T> Get(int id);
        public abstract Task<IEnumerable<T>> GetAll();
        
    }
}
