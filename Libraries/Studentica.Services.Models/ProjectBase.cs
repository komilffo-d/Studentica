using Studentica.Common.Interfaces;
using Studentica.UI.Common.Enums;

namespace Studentica.Services.Models
{
    public interface IProjectBase<T> : IEntity<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        string Name { get; set; }
        StatusProject Status { get; set; }
        DateTimeOffset BeginDate { get; set; }
        DateTimeOffset EndDate { get; set; }
        T OwnerId { get; set; }
        string Description { get; set; }
    }

    public class ProjectBase<T> : ModelBase<T>, IProjectBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public string Name { get; set; } = null!;
        public StatusProject Status { get; set; }
        public DateTimeOffset BeginDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public T OwnerId { get; set; }
        public string Description { get; set; } = null!;
    }
}
