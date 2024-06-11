using Microsoft.AspNetCore.Identity;
using Studentica.Api.Services;
using Studentica.Database.Postgre.Helpers;
using Studentica.Identity.Common;
using Studentica.Identity.Common.Helpers;
using Studentica.Infrastructure.Database;
using Studentica.Infrastructure.Database.Repository.Project;
using Studentica.Infrastructure.Database.Repository.Request;
using Studentica.Infrastructure.Database.Repository.ThirdEntity;
using Studentica.Infrastructure.Database.Repository.User;
using Studentica.Services.Common;

namespace Studentica.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddPostgre<ApiContext>()
                .AddScoped<IProjectRepository<Guid>, ProjectRepository<Guid>>()
                .AddScoped<IRequestRepository<Guid>, RequestRepository<Guid>>()
                .AddScoped<IUserRepository<Guid>, UserRepository<Guid>>()
                .AddScoped<IProjectUserRepository<Guid>, ProjectUserRepository<Guid>>()
                .AddScoped<IProjectService<Guid>, ProjectService<Guid>>()
                .AddScoped<IRequestService<Guid>, RequestService<Guid>>()
                .AddScoped<IUserService<Guid>, UserService<Guid>>()
                .AddScoped<IIdentityService<Guid>, IdentityService>();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ApiContext>();

            IdentityHelper.SetKey(builder.Configuration.GetSettings<IdentitySettings>().Key);


            builder.Services.AddJwtAuthentication();

            builder.Services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; })
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MigrateDatabase<ApiContext>();

            app.Run();
        }
    }
}
