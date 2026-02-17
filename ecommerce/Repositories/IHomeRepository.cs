using ecommerce.Models;

namespace ecommerce.Repositories
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Product>> GetPopularProductsAsync(int count);
        Task<IEnumerable<Product>> GetNewArrivalsAsync(int count);
        Task<IEnumerable<Review>> GetReviewsAsync(int count);

        // This must be here so the Controller can see it!
        Task<IEnumerable<Product>> GetFilteredProductsAsync(string? categorySlug, decimal? minPrice, decimal? maxPrice);
    }
}