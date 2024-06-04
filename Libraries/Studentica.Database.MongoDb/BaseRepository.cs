using MongoDB.Driver;
using Studentica.Common.Interfaces;
using Studentica.Database.Common.Repository;
using System.Linq.Expressions;

namespace Studentica.Database.MongoDb
{
    public class RepositoryBase<TEntity, TType> : IRepository<TEntity, TType> where TEntity : class, IEntity<TType>
                                                                                   where TType : struct, IEquatable<TType>, IComparable<TType>
    {
        private readonly IMongoCollection<TEntity> _dbCollection;
        private readonly FilterDefinitionBuilder<TEntity> _filterBuilder = Builders<TEntity>.Filter;

        public RepositoryBase(IMongoDatabase database, string collectionName)
        {
            _dbCollection = database.GetCollection<TEntity>(collectionName);
        }

        public async Task<long> GetItemsCountAsync() =>
            await _dbCollection.CountDocumentsAsync(_filterBuilder.Empty);
        public async Task<long> GetItemsCountAsync(Expression<Func<TEntity, bool>> filter) =>
            await _dbCollection.CountDocumentsAsync(filter);


        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync() =>
            await _dbCollection.Find(_filterBuilder.Empty).SortBy(x => x.Id).ToListAsync();

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter) =>
            await _dbCollection.Find(filter).SortBy(x => x.Id).ToListAsync();

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(IEnumerable<TType> ids) =>
            await _dbCollection.Find(x => ids.Contains(x.Id)).SortBy(x => x.Id).ToListAsync();

        public async Task<TEntity?> GetAsync(TType id)
        {
            var filter = _filterBuilder.Eq(entity => entity.Id, id);
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter) =>
            await _dbCollection.Find(filter).FirstOrDefaultAsync();

        public async Task CreateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _dbCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var filter = _filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await _dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task RemoveAsync(TType id)
        {
            var filter = _filterBuilder.Eq(entity => entity.Id, id);
            await _dbCollection.DeleteOneAsync(filter);
        }
    }
}
