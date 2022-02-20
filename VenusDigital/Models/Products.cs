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
        public string ProductTitle { get; set; }
        [Required]
        [MaxLength(1500)]
        public string ProductShortDescription { get; set; }
        [Required]
        public string ProductLongDescription { get; set; }
        [Required]
        [MaxLength(150)]
        public string ProductInStock { get; set; }
        [Required]
        public int ProductQuantityInStock { get; set; }
        [Required]
        public decimal ProductMainPrice { get; set; }
        [Required]
        public int GalleryId { get; set; }
        [Required]
        public float ProductScore { get; set; }
        public int TagId { get; set; }
        public int ReviewId { get; set; }
        public decimal ProductOnSalePrice { get; set; }
        public int SalePercent { get; set; }
        [Required]
        public int FeatureId { get; set; }
        [Required]
        public int ItemId { get; set; }
        public DateTime CreateDate { get; set; }

        //Nav
        public List<ProductGalleries> ProductGalleries { get; set; }
        public List<Features> Features { get; set; }
        public List<Tags> Tags { get; set; }
        public List<Reviews> Reviews { get; set; }
        public List<Items> Items { get; set; }
        public List<SelectedCategory> SelectedCategory { get; set; }
    }
}
