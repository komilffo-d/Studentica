using Studentica.Database.Common.Repository;
using Studentica.Database.Postgre.Models;

namespace Studentica.Infrastructure.Database.Repository.Project
{
    public interface IProjectRepository<T> : IRepository<ProjectPostgreBase<T>, T> where T : struct, IEquatable<T>, IComparable<T>
    {

    }
}
