using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;
using Studentica.UI.Handlers;
using Syncfusion.Blazor;

namespace Studentica.UI.Services
{
    public static class CustomServicesCollection
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomEnd;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 3000;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });
            services.AddSyncfusionBlazor();
            services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));
#if DEBUG
            services.AddSassCompiler();
#endif

            services.AddScoped<AuthService>();

            services.AddScoped<TokenServerAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<TokenServerAuthenticationStateProvider>());
            services.AddHttpClient("Ocelot", options =>
            {
                options.BaseAddress = new Uri("https://localhost:8000");
            });

            return services;
        }
    }
}
