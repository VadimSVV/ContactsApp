using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефон обязателен")]
        [Phone(ErrorMessage = "Неверный формат телефона")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Формат: +375291234567")]
        public string MobilePhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Должность обязательна")]
        [StringLength(200)]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Дата рождения обязательна")]
        public DateTime BirthDate {get; set; } = DateTime.Now;
    }
}