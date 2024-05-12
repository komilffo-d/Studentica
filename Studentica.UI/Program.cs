using BitzArt.Blazor.Cookies;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Studentica.UI.Services;
using Studentica.UI.Shared.Core;
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

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "OutsideJwtAuthenticationScheme";
                
            }).AddScheme<OutsideJwtAuthenticationSchemeOptions, OutsideJwtAuthenticationHandler>("OutsideJwtAuthenticationScheme", options => { });


            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddCustomServices();
            builder.AddBlazorCookies();
            var app = builder.Build();

            app.UseRequestLocalization("ru-RU");

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePagesWithRedirects("/404");
            app.UseAntiforgery();


            app.UseStaticFiles();


            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }

}
