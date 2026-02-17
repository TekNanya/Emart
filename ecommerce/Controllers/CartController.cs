using ecommerce.Data;
using ecommerce.Extensions;
using ecommerce.Models;
using ecommerce.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ApplicationDbContext _context;

        public CartController(IProductRepository productRepo, ApplicationDbContext context)
        {
            _productRepo = productRepo;
            _context = context;
        }

        // Helper to get cart from session
        private List<CartItem> GetCartItems()
        {
            return HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        }

        // Helper to save cart and update badge count
        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
            HttpContext.Session.SetInt32("CartCount", cart.Sum(x => x.Quantity));
        }

        public IActionResult Index()
        {
            var items = GetCartItems();

            var viewModel = new CartViewModel
            {
                Items = items,
                TotalPrice = items.Sum(x => x.Price * x.Quantity)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null) return NotFound();

            var cart = GetCartItems();
            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                    ImageUrl = product.ImageUrl
                });
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            SaveCart(cart);

            string referer = Request.Headers["Referer"].ToString();
            return string.IsNullOrEmpty(referer) ? RedirectToAction("Index") : Redirect(referer);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                if (quantity > 0)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    cart.Remove(item);
                }
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var cart = GetCartItems();
            cart.RemoveAll(x => x.ProductId == productId);
            SaveCart(cart);

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.Remove("CartCount");
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Checkout()
        {
            var cartItems = GetCartItems();
            if (cartItems == null || !cartItems.Any()) return RedirectToAction("Index");

            var viewModel = new CheckoutViewModel
            {
                CartItems = cartItems,
                Subtotal = cartItems.Sum(x => x.Price * x.Quantity)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ConfirmOrder(CheckoutViewModel model)
        {
            var cartItems = GetCartItems();

            if (!ModelState.IsValid || cartItems == null || !cartItems.Any())
            {
                model.CartItems = cartItems ?? new List<CartItem>();
                model.Subtotal = model.CartItems.Sum(x => x.Price * x.Quantity);
                return View("Checkout", model);
            }

            // 1. Create the Order object
            var order = new Order
            {
                OrderNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                FirstName = model.FirstName,
                LastName = model.LastName,

                // CRITICAL FIX: Link the order to the logged-in user's identity name (email)
                // This ensures the order appears in "My Orders"
                Email = User.Identity?.Name ?? model.CustomerEmail,

                Address = model.Address,
                OrderDate = DateTime.Now,
                TotalAmount = cartItems.Sum(x => x.Price * x.Quantity) + 500, // + Shipping
                PaymentMethod = model.PaymentMethod,
                Comment = model.Comment,
                Status = "New",
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };

            // 2. Save to Database
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // 3. Clear the Session Cart
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.Remove("CartCount");

            // 4. Redirect to Success Page
            return RedirectToAction("OrderSuccess", new { id = order.Id });
        }

        public IActionResult OrderSuccess(int id)
        {
            return View(id);
        }
    }
}