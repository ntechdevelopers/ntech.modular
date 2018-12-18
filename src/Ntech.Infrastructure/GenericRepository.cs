using Microsoft.EntityFrameworkCore;
using Ntech.Core.Server;
using System.Linq;
using System.Threading.Tasks;

namespace Ntech.Infrastructure
{
    public class GenericRepository<T> : IRepository<T>, IAsyncRepository<T> where T : class
    {
        #region Variables

        private readonly BaseDataContext baseDataContext;

        private DbSet<T> dbSet => this.baseDataContext.Set<T>();

        #endregion

        #region Default constructor

        public GenericRepository(BaseDataContext dataContext)
        {
            this.baseDataContext = dataContext;
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
            var dbEntityEntry = this.baseDataContext.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void SaveChange()
        {
            this.baseDataContext.SaveChanges();
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
