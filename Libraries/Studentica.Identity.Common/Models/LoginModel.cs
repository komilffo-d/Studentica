using System.ComponentModel.DataAnnotations;

namespace Studentica.Identity.Common.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Имя пользователя обязательно!")]
        [MinLength(8, ErrorMessage = "Длина имени должна быть от {1} символов")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Пароль обязателен!")]
        [MinLength(10, ErrorMessage = "Длина пароля должна быть от {1} символов")]
        public string? Password { get; set; }
    } 
}
