using Studentica.Database.Common.Repository;
using Studentica.Database.Postgre.Models;

namespace Studentica.Infrastructure.Database.Repository.User
{
    public interface IUserRepository<T> : IRepository<UserPostgreBase<T>, T> where T : struct, IEquatable<T>, IComparable<T>
    {

    }
}
