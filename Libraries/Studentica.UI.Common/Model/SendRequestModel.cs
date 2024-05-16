using System.ComponentModel.DataAnnotations;

namespace Studentica.UI.Common.Model
{
    public class SendRequestModel
    {
        [Required(ErrorMessage = "Требуется указать имя!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Требуется указать фамилию!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Требуется указать отчество!")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Требуется указать электронную почту!")]
        [EmailAddress(ErrorMessage = "Электронная почта имеет неверный формат!")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Требуется указать номер группы!")]
        public string NumberGroup { get; set; }

        [Required(ErrorMessage = "Требуется указать наименование университета!")]
        public string NameUniversity { get; set; } = "Кубанский государственный аграрный университет имени И.Т. Трубилина";
    }
}
