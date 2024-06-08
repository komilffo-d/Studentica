using System.ComponentModel.DataAnnotations;

namespace Studentica.Identity.Common.Models
{
    public class ResponseModel
    {
        
        public string Token { get; set; }

        public DateTimeOffset? Expiration { get; set; }

        [Required]
        public string? Status { get; set; }
        [Required]
        public string? Message { get; set; }
    }
}
