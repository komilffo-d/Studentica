using Studentica.Database.Common;
using Studentica.Database.Postgre.Models;

namespace Studentica.Infrastructure.Database.Repository.Project
{

    public class IdentityRepository<T> : RepositoryBase<ProjectPostgreBase<T>, T>, IidentityRepository<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public IdentityRepository(ApiContext dbContext) : base(dbContext)
        {

        }
    }
}
