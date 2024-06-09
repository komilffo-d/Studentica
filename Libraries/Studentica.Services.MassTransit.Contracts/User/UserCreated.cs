using Studentica.Identity.Common;

namespace Studentica.Services.MassTransit.Contracts.User
{
    public record UserCreated<T>(
        T UserId,
        string UserName,
        string Password,
        UserRoles Role,
        Guid RequestId) where T : struct, IEquatable<T>, IComparable<T>;
}
    
