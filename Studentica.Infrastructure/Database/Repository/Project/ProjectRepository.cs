using Studentica.Database.Common;
using Studentica.Database.Postgre.Models;

namespace Studentica.Infrastructure.Database.Repository.Project
{

    public class ProjectRepository<T> : RepositoryBase<ProjectPostgreBase<T>, T>, IProjectRepository<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public ProjectRepository(ApiContext dbContext) : base(dbContext)
        {

        }
    }
}
