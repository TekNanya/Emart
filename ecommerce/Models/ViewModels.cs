using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class CartViewModel
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal TotalPrice { get; set; }
    }

   
        public class CheckoutViewModel
        {
            // Data for the order summary (Right side of your screen)
            public List<CartItem> CartItems { get; set; } = new List<CartItem>();
            public decimal Subtotal { get; set; }

            // Data from the form (Left side of your screen)
            [Required(ErrorMessage = "Введите имя")]
            public string FirstName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Введите фамилию")]
            public string LastName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Введите email")]
            [EmailAddress]
            public string CustomerEmail { get; set; } = string.Empty;

            [Required(ErrorMessage = "Укажите адрес доставки")]
            public string Address { get; set; } = string.Empty;

            public string PaymentMethod { get; set; } = "Card";
            public string? Comment { get; set; }
        }
    
}