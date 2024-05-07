using System.ComponentModel.DataAnnotations;

namespace Studentica.Identity.Database.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Имя пользователя обязательно!")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Пароль обязателен!")]
        public string? Password { get; set; }
    }
}
