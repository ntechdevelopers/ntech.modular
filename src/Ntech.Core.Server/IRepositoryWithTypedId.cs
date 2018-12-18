using System.Linq;

namespace Ntech.Core.Server
{
    public interface IRepositoryWithTypedId<T, in TId>
    {
        IQueryable<T> Query();

        void Add(T entity);

        void SaveChange();

        void Remove(T entity);
    }
}
