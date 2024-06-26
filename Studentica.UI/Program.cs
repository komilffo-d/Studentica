using BitzArt.Blazor.Cookies;
using Studentica.Api.Client;
using Studentica.Services.Common;
using Studentica.UI.Handlers;
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


            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            //����������� ������� �������������� � ����������� �� ������ JWT-�������
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "OutsideJwtAuthenticationScheme";

            }).AddScheme<OutsideJwtAuthenticationSchemeOptions, OutsideJwtAuthenticationHandler>("OutsideJwtAuthenticationScheme", options => { });


            builder.Services.AddKeyedSingleton<IApiClient<Guid>, ApiClient<Guid>>("ApiClientGuid", (provider, serviceKey) =>
                new ApiClient<Guid>(builder.Configuration.GetSettings<ServiceSettings>().GatewayPath, string.Empty));



            //���������� ���������� ����������
            builder.Services.AddMemoryCache();
            builder.AddBlazorCookies();
            builder.Services.AddCustomServices();




            var app = builder.Build();


            app.UseRequestLocalization("ru-RU");
            if (!app.Environment.IsDevelopment())
                app.UseHsts();


            app.UseHttpsRedirection();

            app.UseAntiforgery();


            app.UseStaticFiles();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }

}
