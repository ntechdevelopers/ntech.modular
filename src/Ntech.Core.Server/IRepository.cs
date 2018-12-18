using Ntech.Contract.Entity;
using System.Linq;

namespace Ntech.Core.Server
{
    public interface IRepository<T> : IEntity, IRepositoryWithTypedId<T, long>
    {
        IQueryable<T> Entities { get; }

        void Add(T entity);

        T GetById(int id);

        void Remove(T entity);

        void Update(T entity);
    }
}
