using System.Collections.Generic;

namespace ecommerce.Models // namespace matches your folder exactly
{
    public class ShopViewModel
    {
        // The list of products to display in the grid
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        // The list of categories for the sidebar (Ordered by DisplayOrder)
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();

        // Tracks which category is currently selected to highlight the sidebar
        public string? SelectedCategorySlug { get; set; }

        // Price filtering properties
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}