using Microsoft.EntityFrameworkCore;
using Studentica.Database.Postgre.Models;

namespace Studentica.Infrastructure.Database
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseIdentityColumns();
        }

        public DbSet<ProjectPostgreBase<Guid>> Projets { get; set; }
    }
}
