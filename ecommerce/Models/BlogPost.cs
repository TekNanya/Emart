using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заголовок обязателен")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Контент обязателен")]
        public string Content { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Краткое описание обязательно")]
        public string Summary { get; set; } = string.Empty;
    }
}