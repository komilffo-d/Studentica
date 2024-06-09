using Studentica.Common.Interfaces;
using Studentica.Identity.Common;

namespace Studentica.Services.Models
{
    public interface IUserBase<T> : IEntity<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        string LastName { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        int? NumberCourse { get; set; }
        string? Group { get; set; }
        string Major { get; set; }
        string NameUniversity { get; set; }
        string Email { get; set; }
        public UserRoles Role { get; set; }
    }

    public class UserBase<T> : ModelBase<T>, IUserBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public int? NumberCourse { get; set; }
        public string? Group { get; set; }
        public string Major { get; set; } = null!;
        public string NameUniversity { get; set; } = null!;
        public string Email { get; set; } = null!;
        public UserRoles Role { get; set; }
    }
}
