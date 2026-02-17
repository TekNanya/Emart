using ecommerce.Data;
using ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetPopularProductsAsync(int count)
        {
            return await _context.Products
                .Include(p => p.Category)
                // Note: Ensure 'IsPopular' exists in your Product.cs model
                .Where(p => p.IsPopular)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetNewArrivalsAsync(int count)
        {
            return await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsAsync(int count)
        {
            return await _context.Reviews
                .OrderByDescending(r => r.Date)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(string? categorySlug, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(categorySlug))
            {
                query = query.Where(p => p.Category.Slug == categorySlug);
            }

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(
    string? categorySlug,
    decimal? minPrice,
    decimal? maxPrice,
    string? searchTerm = null,
    string? sortOrder = null)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            // 1. Search Filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            // 2. Category Filter
            if (!string.IsNullOrEmpty(categorySlug))
            {
                query = query.Where(p => p.Category.Slug == categorySlug);
            }

            // 3. Price Filter
            if (minPrice.HasValue) query = query.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue) query = query.Where(p => p.Price <= maxPrice.Value);

            // 4. Sorting Logic (Matches image_9369c3.png)
            query = sortOrder switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "name" => query.OrderBy(p => p.Name),
                _ => query.OrderByDescending(p => p.CreatedDate) // Default: Сначала новые
            };

            return await query.ToListAsync();
        }
    }
}