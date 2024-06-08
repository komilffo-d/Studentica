using Microsoft.EntityFrameworkCore;
using Studentica.Database.Postgre.Models;
using Studentica.Infrastructure.Database.Configuration;

namespace Studentica.Infrastructure.Database
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.UseIdentityColumns();
        }

        public DbSet<ProjectPostgreBase<Guid>> Projects { get; set; }

        public DbSet<UserPostgreBase<Guid>> Users { get; set; }
    }
}
