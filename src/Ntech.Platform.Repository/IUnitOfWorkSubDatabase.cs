using Ntech.Contract.Entity.SubDatabase;
using Ntech.Core.Server;

namespace Ntech.Platform.Repository
{
    public interface IUnitOfWorkSubDatabase: IUnitOfWork
    {
        #region Repositories

        IRepository<Value> ValueRepository { get; }

        #endregion
    }
}
