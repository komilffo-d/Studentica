using Studentica.Services.Models;

namespace Studentica.Database.Postgre.Models
{
    public interface IProjectPostgreBase<T> : IProjectBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        List<UserPostgreBase<T>> Members { get; set; }
    }

    public class ProjectPostgreBase<T> : ProjectBase<T>, IProjectPostgreBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public List<UserPostgreBase<T>> Members { get; set; } = new();
    }
}
