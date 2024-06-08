using Microsoft.EntityFrameworkCore;
using Studentica.Database.Postgre.Converters;
using Studentica.Database.Postgre.Models;
using Studentica.Infrastructure.Database.Configuration;

namespace Studentica.Infrastructure.Database
{
    public class ApiContext : DbContext
    {
        public DbSet<ProjectPostgreBase<Guid>> Projects { get; set; }

        public DbSet<UserPostgreBase<Guid>> Users { get; set; }

        public DbSet<RequestPostgreBase<Guid>> Requests { get; set; }

        public ApiContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.UseIdentityColumns();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<DateTimeOffset>()
                .HaveConversion<DateTimeOffsetConverter>();
        }
    }
}
