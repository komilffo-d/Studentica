using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Studentica.Common.Interfaces;
using Studentica.Database.Common.Repository;
using Studentica.Database.Postgre.Settings;
using Studentica.Services.Common;
using System;

namespace Studentica.Database.Postgre.Helpers
{
    public static class ConfigurationHelper
    {
        public static IServiceCollection AddPostgre<T>(this IServiceCollection serviceCollection) where T : DbContext
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var configuration = serviceProvider.GetService<IConfiguration>()!;
            var serviceSettings = configuration.GetSettings<ServiceSettings>();
            var postgreDbSettings = configuration.GetSettings<PostgreDbSettings>();

            serviceCollection.AddDbContext<T>(opts => opts.UseNpgsql(postgreDbSettings.ConnectionString));
            return serviceCollection;
        }
    }
}
