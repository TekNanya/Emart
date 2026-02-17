using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; } = string.Empty; // Matches your error fix

        [Required]
        public string Comment { get; set; } = string.Empty;

        public int Rating { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        // Add this to link reviews to users
        public string UserId { get; set; } = string.Empty;

        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}