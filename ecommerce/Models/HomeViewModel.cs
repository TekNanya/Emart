namespace ecommerce.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();

        // This handles your "Hits" section
        public IEnumerable<Product> PopularProducts { get; set; } = new List<Product>();

        // This handles your "New Arrivals" section
        public IEnumerable<Product> NewArrivals { get; set; } = new List<Product>();

        // This handles the review slider/grid
        public IEnumerable<Review> CustomerReviews { get; set; } = new List<Review>();
    }
}