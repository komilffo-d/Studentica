using Studentica.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studentica.Database.Postgre.Models
{
    public interface IProjectUserPostgreBase<T>: IEntity<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        T ProjectId { get; set; }
        T UserId { get; set; }
    }

    public class ProjectUserPostgreBase<T>: IProjectUserPostgreBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        [NotMapped]
        public T Id { get; init; }

        public T ProjectId { get; set; }
        public T UserId { get; set; }
    }
}
