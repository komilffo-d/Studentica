using Studentica.Common.Interfaces;
using Studentica.Identity.Common;

namespace Studentica.Common.DTOs.User
{
    public record UserDto<T>(T Id, string LastName, string FirstName, string MiddleName, int? NumberCourse, string? Group, string Major,string NameUniversity, UserRoles Role,  string Email) : IEntity<T> where T : struct, IEquatable<T>, IComparable<T>;
}
