using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Studentica.Identity.Common
{
    public abstract class CustomControllerBase<T> : ControllerBase where T : struct, IParsable<T>
    {
        protected T GetUserId()
        {
            return T.Parse(User.Claims.First(i => i.Type == "UserId").Value, null);
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
