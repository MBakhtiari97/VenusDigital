using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Areas.Admin.Models
{
    public class PurchaseInvoiceViewModel
    {
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public int OrderId { get; set; }
        [Display(Name = "Order Price")]
        public decimal OrderTotalPrice { get; set; }
        [Display(Name = "Product Title")]
        public string ProductTitle { get; set; }
        [Display(Name = "Qyt")]
        public int Count { get; set; }
        [Display(Name = "Single Price")]
        public decimal ProductPrice { get; set; }
    }
}
