using Studentica.Database.Common.Repository;
using Studentica.Database.Postgre.Models;
using Studentica.Identity.Common.Models;

namespace Studentica.Infrastructure.Database.Repository.Identity
{
    public interface IIdentityRepository
    {
        Task<(string, DateTime)?> Login(LoginModel model);

        Task<(bool, string message)> Register(RegisterModel model);

        Task<bool> Validate(string token);
    }
}
