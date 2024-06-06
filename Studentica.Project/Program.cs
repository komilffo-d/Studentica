using Studentica.Common.Interfaces;
using Studentica.Database.MongoDb.Helpers;
using Studentica.Database.MongoDb.Models;
using Studentica.Project.Services;
using Studentica.Services.Models;

namespace Studentica.Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMongo()
                .AddMongoRepository<ProjectMongoBase<Guid>, Guid>("Project");

            builder.Services.AddScoped<IProjectService<Guid>, ProjectService<Guid>>();

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

            app.Run();
        }
    }
}
