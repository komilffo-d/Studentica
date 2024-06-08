using System.ComponentModel.DataAnnotations;

namespace Studentica.Common.DTOs.Requests.Project
{
    public record ProjectCreateRequest([Required]string Name, [Required] DateTimeOffset BeginDate, [Required] DateTimeOffset EndDate, [Required]string Description);
}
