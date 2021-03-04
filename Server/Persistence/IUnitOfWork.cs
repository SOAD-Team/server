using System.Threading.Tasks;

namespace Server.Persistence
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}