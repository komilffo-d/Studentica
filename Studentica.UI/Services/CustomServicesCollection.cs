using MudBlazor.Services;
using Syncfusion.Blazor;

namespace Studentica.UI.Services
{
    public static class CustomServicesCollection
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddMudServices();
            services.AddSyncfusionBlazor();
            services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));
#if DEBUG
            services.AddSassCompiler();
#endif

            services.AddScoped<AuthService>();
            services.AddHttpClient("Ocelot", options =>
            {
                options.BaseAddress = new Uri("https://localhost:8000");
            });

            return services;
        }
    }
}
