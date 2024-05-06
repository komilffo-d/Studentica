using MudBlazor.Services;
using Studentica.UI.Shared.Core;
using Syncfusion.Blazor;
using System.Globalization;

namespace Studentica.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ru-RU");

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddMudServices();

#if DEBUG
            builder.Services.AddSassCompiler();
#endif
            builder.Services.AddSyncfusionBlazor();

            builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));
            var app = builder.Build();

            app.UseRequestLocalization("ru-RU");
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
