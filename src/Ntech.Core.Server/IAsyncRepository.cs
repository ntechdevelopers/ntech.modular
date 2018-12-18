using Ntech.Contract.Entity;
using System.Threading.Tasks;

namespace Ntech.Core.Server
{
    public interface IAsyncRepository<T> : IEntity, IRepositoryWithTypedId<T, long> 
    {
        Task AddAsync(T entity);

        Task<T> GetByIdAsync(int id);

        Task RemoveAsync(T entity);

        Task UpdateAsync(T entity);
    }
}
