using System.ComponentModel.DataAnnotations;

namespace Studentica.Common.DTOs.Requests.Project
{
    public record ProjectCreateRequest([Required]string Name, [Required] DateTime BeginDate, [Required] DateTime EndDate, [Required]string Description);
}
