using Studentica.Identity.Common;

namespace Studentica.Services.MassTransit.Contracts.User
{
    public record UserCreated(
        string UserName,
        string Password,
        UserRoles Role,
        Guid RequestId);
}
    
