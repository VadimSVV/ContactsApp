using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Имя: минимум 2, максимум 100 символов")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефон обязателен")]
        [Phone(ErrorMessage = "Неверный формат телефона")]
        [RegularExpression(@"^\+?[1-9]\d{7,14}$", ErrorMessage = "Формат: +375291234567 (8-15 цифр после +)")]
        public string MobilePhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Должность обязательна")]
        [StringLength(200)]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Дата рождения обязательна")]
        public DateTime BirthDate {get; set; } = DateTime.Now;
    }
}