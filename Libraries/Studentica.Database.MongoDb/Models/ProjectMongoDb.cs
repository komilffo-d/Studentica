using Studentica.Services.Models;

namespace Studentica.Database.MongoDb.Models
{
    public interface IProjectMongo<T> : IProjectBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        ICollection<T> Members { get; set; }
    }

    public class ProjectMongoBase<T> : ProjectBase<T>, IProjectMongo<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public ICollection<T> Members { get; set; } = new List<T>();
    }
}
