using Studentica.Database.Common.Repository;
using Studentica.Database.Postgre.Models;

namespace Studentica.Infrastructure.Database.Repository.Request
{
    public interface IRequestRepository<T> : IRepository<RequestPostgreBase<T>, T> where T : struct, IEquatable<T>, IComparable<T>
    {

    }
}
