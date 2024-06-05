using Studentica.Common.Interfaces;
using Studentica.UI.Common.Enums;

namespace Studentica.Services.Models
{
    public interface IProjectBase<T> : IEntity<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        string Name { get; set; }
        StatusProject Status { get; set; }
        DateTime BeginDate { get; set; }
        DateTime EndDate { get; set; }
        string Description { get; set; }
    }

    public class ProjectBase<T> : ModelBase<T>, IProjectBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public string Name { get; set; } = null!;
        public StatusProject Status { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = null!;
    }
}
