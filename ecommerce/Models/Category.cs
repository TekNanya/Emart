using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? NameEn { get; set; }

        public string? Slug { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public int DisplayOrder { get; set; } = 5;

        public bool IsActive { get; set; } = true;

        // Navigation property for the relationship with Products
        public virtual ICollection<Product>? Products { get; set; }
    }
}