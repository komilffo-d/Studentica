using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Studentica.Identity.Common
{
    public abstract class CustomControllerBase<T> : ControllerBase
    {
        protected Guid GetUserId()
        {
            return Guid.Parse(User.Claims.First(i => i.Type == "UserId").Value);
        }

        protected IReadOnlyCollection<UserRoles> GetRoles()
        {
            var roles = new List<UserRoles>();
            var rolesInToken = User.Claims.Where(x => x.Type == "Role");
            foreach (var claim in rolesInToken)
            {
                if (!Enum.TryParse<UserRoles>(claim.Value, out var role) || roles.Contains(role))
                    continue;
                roles.Add(role);
            }
            return roles.AsReadOnly();
        }
    }
}
