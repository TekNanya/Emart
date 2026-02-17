using ecommerce.Models;
using ecommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;
        private readonly IProductRepository _productRepository; // Added this

        // Inject both repositories into the constructor
        public HomeController(
            ILogger<HomeController> logger,
            IHomeRepository homeRepository,
            IProductRepository productRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                Categories = await _homeRepository.GetCategoriesAsync(),
                // These now pull from the Product Repository
                PopularProducts = await _productRepository.GetPopularAsync(4),
                NewArrivals = await _productRepository.GetNewArrivalsAsync(4),
                CustomerReviews = await _homeRepository.GetReviewsAsync(3)
            };

            return View(model);
        }

        [Route("Shop")]
        public async Task<IActionResult> Shop(string? category, decimal? minPrice, decimal? maxPrice, string? searchTerm, string? sortOrder)
        {
            var model = new ShopViewModel
            {
                // Logic moved to Product Repository
                Products = await _productRepository.GetFilteredProductsAsync(category, minPrice, maxPrice, searchTerm, sortOrder),

                // Categories stay in Home Repository (shared site-wide data)
                Categories = await _homeRepository.GetCategoriesAsync(),

                SelectedCategorySlug = category,
                MinPrice = minPrice,
                MaxPrice = maxPrice
                // If you add SearchTerm/SortOrder to ShopViewModel, set them here too
            };

            return View(model);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("Product/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            // Use the repository method we created earlier
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null) return NotFound();

            // Get related products from the same category for the bottom section
            ViewBag.RelatedProducts = await _productRepository.GetFilteredProductsAsync(product.Category?.Slug, null, null);

            return View(product);
        }


        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Here you would normally send an email or save to DB
                TempData["SuccessMessage"] = "Спасибо! Ваше сообщение отправлено. Мы свяжемся с вами в ближайшее время.";
                return RedirectToAction("Contact");
            }
            return View(model);
        }
    }
}