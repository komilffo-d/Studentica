using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Studentica.Database.Postgre.Converters;
using Studentica.Database.Postgre.Models;
using Studentica.Infrastructure.Database.Configuration;

namespace Studentica.Infrastructure.Database
{
    public class ApiContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<ProjectPostgreBase<Guid>> ProjectsData { get; set; }

        public DbSet<UserPostgreBase<Guid>> UsersData { get; set; }

        public DbSet<RequestPostgreBase<Guid>> RequestsData { get; set; }

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
