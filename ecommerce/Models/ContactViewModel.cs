using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Пожалуйста, введите ваше имя")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите тему сообщения")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Напишите ваше сообщение")]
        [StringLength(1000, MinimumLength = 10)]
        public string Message { get; set; } = string.Empty;
    }
}