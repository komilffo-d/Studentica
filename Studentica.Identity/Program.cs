using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Studentica.Identity.Common;
using Studentica.Identity.Common.Helpers;
using Studentica.Identity.Database;
using Studentica.Services.Common;

namespace Studentica.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var connectionString = string.Format(builder.Configuration.GetConnectionString("Database")!,
                builder.Configuration["Database:Name"],
                builder.Configuration["Database:Login"],
                builder.Configuration["Database:Password"])
            ;
            builder.Services.AddDbContext<AuthContext>(options => options.UseNpgsql(connectionString));

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

            app.Run();
        }
    }
}
