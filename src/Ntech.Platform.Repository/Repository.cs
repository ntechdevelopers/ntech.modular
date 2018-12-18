using Microsoft.EntityFrameworkCore;
using Ntech.Contract.Entity;
using Ntech.Core.Server;
using Ntech.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Ntech.Platform.Repository
{
    public class Repository<T> : IRepository<T>, IAsyncRepository<T> where T : class, IEntity
    {
        #region Variables

        private readonly ModuleAPIDataContext dataContext;

        private DbSet<T> dbSet => this.dataContext.Set<T>();

        #endregion

        #region Default constructor

        public Repository(ModuleAPIDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        #endregion

        #region Implement IRepository

        public IQueryable<T> Entities => this.dbSet;

        public void Add(T entity)
        {
            this.dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            this.dbSet.Remove(entity);
        }

        public T GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        public void Update(T entity)
        {
            var dbEntityEntry = this.dataContext.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void SaveChange()
        {
            this.dataContext.SaveChanges();
        }

        public IQueryable<T> Query()
        {
            return this.dbSet;
        }


        #endregion

        #region Implement IAsyncRepository

        public Task AddAsync(T entity)
        {
            return Task.Run(() =>
            {
                this.Add(entity);
            });
        }

        public Task<T> GetByIdAsync(int id)
        {
            return Task<T>.Factory.StartNew(() =>
            {
                return this.GetById(id);
            });
        }

        public Task RemoveAsync(T entity)
        {
            return Task.Run(() =>
            {
                this.Remove(entity);
            });
        }

        public Task UpdateAsync(T entity)
        {
            return Task.Run(() =>
            {
                this.Update(entity);
            });
        }

        #endregion

    }
}
