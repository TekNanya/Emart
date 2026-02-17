using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; } 
       

        [Required, Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required, Display(Name = "Street Address")]
        public string StreetAddress { get; set; } = string.Empty;

        public string ApartmentOrSuite { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string StateOrProvince { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; } = string.Empty;

        public bool IsDefault { get; set; }
  
}
}
