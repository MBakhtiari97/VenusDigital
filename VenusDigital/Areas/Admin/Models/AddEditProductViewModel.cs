using System;
using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Areas.Admin.Models
{
    public class AddEditProductViewModel
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "Title")]
        public string ProductTitle { get; set; }
        [Display(Name = "Introduce")]
        [Required]
        [MaxLength(1500)]
        public string ProductShortDescription { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string ProductLongDescription { get; set; }
        [Required]
        [MaxLength(150)]
        [Display(Name = "Status")]
        public string ProductInStock { get; set; }
        [Required]
        [Display(Name = "Qunatity")]
        public int ProductQuantityInStock { get; set; }
        [Required]
        [Display(Name = "Price($)")]
        public decimal ProductMainPrice { get; set; }
        [Required]
        [Display(Name = "Score")]
        public float ProductScore { get; set; }
        [Display(Name = "Sale Price($)")]
        public decimal ProductOnSalePrice { get; set; }
        [Display(Name = "Sale Percent")]
        public int SalePercent { get; set; }
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }
    }
}
