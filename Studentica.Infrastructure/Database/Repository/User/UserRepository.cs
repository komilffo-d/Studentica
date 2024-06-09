using Studentica.Database.Common;
using Studentica.Database.Postgre.Models;

namespace Studentica.Infrastructure.Database.Repository.User
{
    public class UserRepository<T> : RepositoryBase<UserPostgreBase<T>, T>, IUserRepository<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public UserRepository(ApiContext dbContext) : base(dbContext)
        {

        }
    }
}
