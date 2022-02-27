using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Models
{
    public class Products
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
        public int GalleryId { get; set; }
        [Required]
        [Display(Name = "Score")]
        public float ProductScore { get; set; }
        public int TagId { get; set; }
        public int ReviewId { get; set; }
        [Display(Name = "Sale Price($)")]
        public decimal ProductOnSalePrice { get; set; }
        [Display(Name = "Sale Percent")]
        public int SalePercent { get; set; }
        [Required]
        public int FeatureId { get; set; }
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        //Nav
        public List<ProductGalleries> ProductGalleries { get; set; }
        public List<Features> Features { get; set; }
        public List<Tags> Tags { get; set; }
        public List<Reviews> Reviews { get; set; }
        public List<SelectedCategory> SelectedCategory { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public List<Compare> Compare { get; set; }
    }
}
