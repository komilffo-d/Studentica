using Microsoft.AspNetCore.Identity;
using Studentica.Database.Postgre.Helpers;
using Studentica.Identity.Common;
using Studentica.Identity.Common.Helpers;
using Studentica.Infrastructure.Database;
using Studentica.Infrastructure.Database.Repository.Identity;
using Studentica.Infrastructure.Database.Repository.User;
using Studentica.Services.Common;
using Studentica.Services.MassTransit.RabbitMq;

namespace Studentica.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddPostgre<AuthContext>();


            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddEntityFrameworkStores<AuthContext>();

            IdentityHelper.SetKey(builder.Configuration.GetSettings<IdentitySettings>().Key);

            builder.Services
                .AddScoped<IIdentityRepository, IdentityRepository>()
                .AddMassTransitWithRabbitMq();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();


            builder.Services.AddSwaggerGen();

            builder.Configuration.AddUserSecrets<Program>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.MigrateDatabase<AuthContext>();

            app.Run();
        }
    }
}
