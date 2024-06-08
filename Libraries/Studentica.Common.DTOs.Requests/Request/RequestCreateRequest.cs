using System.ComponentModel.DataAnnotations;

namespace Studentica.Common.DTOs.Requests.Request
{
    public record RequestCreateRequest([Required]string LastName, [Required]string FirstName, [Required]string MiddleName, [Required]string Email, [Required]string NumberGroup, [Required]string NameUniversity);
}
