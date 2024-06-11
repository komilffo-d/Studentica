using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;
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
                .UsingEntity<ProjectUserPostgreBase<Guid>>("Projects_XRef_Users",
                l => l.HasOne<UserPostgreBase<Guid>>().WithMany().HasForeignKey(th => th.UserId).OnDelete(DeleteBehavior.Restrict),
                r => r.HasOne<ProjectPostgreBase<Guid>>().WithMany().HasForeignKey(th=>th.ProjectId).OnDelete(DeleteBehavior.Restrict));
        }
    }
}
