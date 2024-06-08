using Studentica.Services.Models;

namespace Studentica.Database.Postgre.Models
{
    public interface IProjectPostgre<T> : IProjectBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        ICollection<T> Members { get; set; }
    }

    public class ProjectPostgreBase<T> : ProjectBase<T>, IProjectPostgre<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public ICollection<T> Members { get; set; } = new List<T>();
    }
}
