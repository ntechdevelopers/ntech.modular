using Microsoft.EntityFrameworkCore;
using Ntech.Core.Server;
using System.Linq;
using System.Threading.Tasks;

namespace Ntech.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Variables

        private readonly BaseDataContext baseDataContext;

        #endregion

        #region Implement IUnitOfWork

        public UnitOfWork(BaseDataContext dataContext)
        {
            this.baseDataContext = dataContext;
        }

        public void Commit()
        {
            this.baseDataContext.SaveChanges();
        }

        public Task CommitAsync()
        {
            return this.baseDataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.baseDataContext.Dispose();
        }

        public void RejectChanges()
        {
            var entries = this.baseDataContext.ChangeTracker.Entries();
            var changeTrackers = entries.Where(e => e.State != EntityState.Unchanged);
            foreach (var entry in changeTrackers)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        #endregion
    }
}
