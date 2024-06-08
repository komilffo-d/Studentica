using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studentica.Database.Postgre.Models;

namespace Studentica.Infrastructure.Database.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<ProjectPostgreBase<Guid>>
    {
        public void Configure(EntityTypeBuilder<ProjectPostgreBase<Guid>> builder)
        {
            builder
                .HasMany(p => p.Members)
                .WithMany(u => u.Projects)
                .UsingEntity(e => e.ToTable("Projects_XRef_Users"));
        }
    }
}
