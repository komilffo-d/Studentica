using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Studentica.Common.Interfaces;
using Studentica.Database.Common.Repository;
using Studentica.Database.MongoDb.Settings;
using Studentica.Services.Common;

namespace Studentica.Database.MongoDb.Helpers
{
    public static class ConfigurationHelper
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>()!;
                var serviceSettings = configuration.GetSettings<ServiceSettings>();
                var mongoDbSettings = configuration.GetSettings<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            return services;
        }

        public static IServiceCollection AddMongoRepository<TEntity, TType>(this IServiceCollection services, string collectionName)
            where TEntity : class, IEntity<TType>
            where TType : struct, IEquatable<TType>, IComparable<TType>
        {
            services.AddSingleton<IRepository<TEntity, TType>>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>()!;
                return new RepositoryBase<TEntity,TType>(database, collectionName);
            });

            return services;
        }
    }
}
