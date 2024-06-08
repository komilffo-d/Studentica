using Studentica.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studentica.Services.Models
{
    public interface IRequestBase<T> : IEntity<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        string LastName { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string Email { get; set; }
        string NumberGroup { get; set; }
        string NameUniversity { get; set; }
        int NumberRequest { get; set; }
        DateTimeOffset CreatedDate { get; set; }
    }

    public class RequestBase<T> : ModelBase<T>, IRequestBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NumberGroup { get; set; } = null!;
        public string NameUniversity { get; set; } = null!;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NumberRequest { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
