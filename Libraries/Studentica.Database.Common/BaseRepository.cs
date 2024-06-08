using Microsoft.EntityFrameworkCore;
using Studentica.Common.Interfaces;
using Studentica.Database.Common.Misc;
using Studentica.Database.Common.Repository;
using System.Linq.Expressions;

namespace Studentica.Database.Common
{
    public abstract class RepositoryBase<TEntity, TType> : IRepository<TEntity, TType> where TEntity : class, IEntity<TType>
                                                                                   where TType : struct, IEquatable<TType>, IComparable<TType>
    {
        private readonly DbContext _dbContext;

        protected RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> sortBy,
            int? skip, int? take)
        {
            return _dbContext.Set<TEntity>().AsNoTracking().Where(filter).OrderBy(sortBy).Skip(skip ?? DefaultValues.Skip)
                .Take(take ?? DefaultValues.Take);
        }

        public async Task<TEntity?> GetAsync(TType id)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var created = await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return created.Entity;
        }

        public async Task<TEntity> UpdateAsync(TType id, TEntity entity)
        {
            var updated = _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task DeleteAsync(TType id)
        {
            var entity = await GetAsync(id);
            if (entity == null) return;
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
