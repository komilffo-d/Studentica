using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Studentica.Services.Common;
using System.Reflection;

namespace Studentica.Services.MassTransit.RabbitMq
{
    public static class MassTransitHelper
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumers(Assembly.GetEntryAssembly());

                configure.UsingRabbitMq((context, configurator) =>
                {
                    var configuration = context.GetService<IConfiguration>()!;

                    var serviceSettings = configuration.GetSettings<ServiceSettings>();

                    var rabbitMqSettings = configuration.GetSettings<RabbitMqSettings>();

                    configurator.Host(rabbitMqSettings.Host);

                    configurator.ConfigureEndpoints(context,
                        new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));
                    configurator.UseMessageRetry(retryConfigurator =>
                    {
                        retryConfigurator.Interval(3, TimeSpan.FromSeconds(5));
                    });
                });
            });

            services.AddOptions<MassTransitHostOptions>();

            return services;
        }
    }
}
