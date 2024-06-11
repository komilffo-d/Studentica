using Studentica.Database.Common.Repository;
using Studentica.Database.Postgre.Models;

namespace Studentica.Infrastructure.Database.Repository.ThirdEntity
{
    public interface IProjectUserRepository<T> : IRepository<ProjectUserPostgreBase<T>, T> where T : struct, IEquatable<T>, IComparable<T>
    {

    }
}
