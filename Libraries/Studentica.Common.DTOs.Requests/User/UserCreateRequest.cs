using Studentica.Identity.Common;
using System.ComponentModel.DataAnnotations;

namespace Studentica.Common.DTOs.Requests.User
{
    public record UserCreateRequest([Required] string LastName, [Required] string FirstName, [Required] string MiddleName, int? NumberCourse, string? Group, [Required] string Major, [Required] string NameUniversity, [Required] UserRoles Role, [Required] string Email, [Required] string UserName, [Required]string Password, [Required]Guid RequestId);
}
