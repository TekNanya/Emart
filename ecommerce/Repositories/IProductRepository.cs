using ecommerce.Models;

namespace ecommerce.Repositories
{
    public interface IProductRepository
    {
        // The core "Shop" logic lives here now
        Task<IEnumerable<Product>> GetFilteredProductsAsync(
            string? categorySlug,
            decimal? minPrice,
            decimal? maxPrice,
            string? searchTerm = null,
            string? sortOrder = null);

        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetPopularAsync(int count);
        Task<IEnumerable<Product>> GetNewArrivalsAsync(int count);
    }
}