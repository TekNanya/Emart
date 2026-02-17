using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        // Fixes the 'Weight' errors
        public string? Weight { get; set; }

        // Fixes the 'Stock' errors
        public int Stock { get; set; }

        // Fixes the 'IsActive' errors
        public bool IsActive { get; set; } = true;

        // Properties we discussed for sorting and badges
        public bool IsPopular { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Relationship with Category
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}