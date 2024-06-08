using Studentica.Common.Interfaces;
using System.Linq.Expressions;

namespace Studentica.Database.Common.Repository
{
    public interface IRepository<TEntity, TType> where TEntity : class, IEntity<TType>
                                             where TType : struct, IEquatable<TType>, IComparable<TType>
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> sortBy,
            int? skip, int? take);

        Task<TEntity?> GetAsync(TType id);

        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TType id, TEntity entity);

        Task DeleteAsync(TType id);
    }
}
