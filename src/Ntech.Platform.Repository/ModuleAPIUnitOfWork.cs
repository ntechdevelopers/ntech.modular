using Ntech.Contract.Entity.SubDatabase;
using Ntech.Core.Server;
using Ntech.Infrastructure;
using System.Threading.Tasks;

namespace Ntech.Platform.Repository
{
    public class ModuleAPIUnitOfWork : UnitOfWork, IUnitOfWorkSubDatabase
    {
        private readonly ModuleAPIDataContext baseDataContext;

        public ModuleAPIUnitOfWork(ModuleAPIDataContext dataContext) : base(dataContext)
        {
            this.baseDataContext = dataContext;
        }

        public IRepository<Value> ValueRepository => new Repository<Value>(baseDataContext);

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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
