using Microsoft.AspNetCore.Mvc;
using Studentica.Identity.Common.Models;

namespace Studentica.Api.Identity
{
    public interface IIdentityApi<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<ObjectResult> Login(LoginModel model);

        Task<ObjectResult> Register(RegisterModel model);

        Task<ObjectResult> Validate(string token);
    }
}
