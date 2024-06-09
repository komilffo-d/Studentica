using Studentica.Common.Interfaces;

namespace Studentica.Common.DTOs.Request
{
    public record RequestDto<T>(T Id, string LastName, string FirstName, string MiddleName, string Email, string Group, string NameUniversity, int NumberRequest, DateTimeOffset CreatedDate) : IEntity<T> where T : struct, IEquatable<T>, IComparable<T>;
}
