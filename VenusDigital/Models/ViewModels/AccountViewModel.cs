using System;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace VenusDigital.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address!")]
        [MaxLength(250)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must contain character and numbers")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [Compare("Password",ErrorMessage = "Password and RePassword does not match")]
        public string RePassword { get; set; }
        [Display(Name = "Username")]
        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }
        [Display(Name = "Phone Number")]
        [MaxLength(250)]
        public string PhoneNumber { get; set; }


    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address!")]
        [MaxLength(250)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class WishlistViewModel
    {
        [Required]
        [MaxLength(250)]
        public string ProductName { get; set; }
        [Required]
        [MaxLength(50)]
        public string ProductImage { get; set; }
        [Required]
        public float ProductScore { get; set; }
        [Required]
        public decimal ProductMainPrice { get; set; }
        public decimal ProductOffPrice { get; set; }
        [Required]
        public int ReviewsCount { get; set; }
        [Required]
        public int QuantityInStock { get; set; }

    }

    public class RecoverPasswordViewModel
    {
        public string Email { get; set; }
    }
}
