using System.Threading.Tasks;

namespace Ntech.Core.Server
{
    public interface IUnitOfWork
    {
        void Commit();

        Task CommitAsync();

        void RejectChanges();

        void Dispose();

    }
}
