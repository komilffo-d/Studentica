using System.ComponentModel.DataAnnotations;

namespace Studentica.Identity.Database.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Имя пользователя обязательно!")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Электронная почта обязательна!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен!")]
        public string? Password { get; set; }
    }
}
