using Studentica.Common.Interfaces;
using System.Linq.Expressions;

namespace Studentica.Database.Common.Repository
{
    public interface IRepository<TEntity, TType> where TEntity : class, IEntity<TType>
                                             where TType : struct, IEquatable<TType>, IComparable<TType>
    {
        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
        Task<IReadOnlyCollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<IReadOnlyCollection<TEntity>> GetAllAsync(IEnumerable<TType> ids);
        Task<TEntity?> GetAsync(TType id);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TType id);
        Task<long> GetItemsCountAsync();
        Task<long> GetItemsCountAsync(Expression<Func<TEntity, bool>> filter);
    }
}
