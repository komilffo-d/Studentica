using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Studentica.Identity.Common.Helpers
{
    public static class IdentityHelper
    {
        private static string? _key;
        public static SymmetricSecurityKey SecurityKey => new(
            Encoding.ASCII.GetBytes(_key ?? throw new Exception("Ключ пустой!Используйте метод SetKey() для инициализации!")));

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = GetValidationParameters();
                }
            );

            return services;
        }

        public static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {

                ValidateIssuerSigningKey = true,
                ValidateLifetime=true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = SecurityKey
            };
        }

        public static void SetKey(string key)
        {
            _key = key;
        }
    }
}
