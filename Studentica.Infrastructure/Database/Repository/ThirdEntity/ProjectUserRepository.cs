using Studentica.Database.Common;
using Studentica.Database.Postgre.Models;

namespace Studentica.Infrastructure.Database.Repository.ThirdEntity
{

    public class ProjectUserRepository<T> : RepositoryBase<ProjectUserPostgreBase<T>, T>, IProjectUserRepository<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public ProjectUserRepository(ApiContext dbContext) : base(dbContext)
        {

        }
    }
}
