using Studentica.Database.Common;
using Studentica.Database.Postgre.Models;

namespace Studentica.Infrastructure.Database.Repository.Request
{
    public class RequestRepository<T> : RepositoryBase<RequestPostgreBase<T>, T>, IRequestRepository<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public RequestRepository(ApiContext dbContext) : base(dbContext)
        {

        }
    }
}
