using System.Threading.Tasks;

namespace Server.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MoviesDB context;
        public UnitOfWork(MoviesDB context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
