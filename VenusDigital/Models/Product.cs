using System;
using System.Collections.Generic;

#nullable disable

namespace VenusDigital.Models
{
    public partial class Product
    {
        public Product()
        {
            Categories = new HashSet<Category>();
            Features = new HashSet<Feature>();
            Items = new HashSet<Item>();
            ProductGalleries = new HashSet<ProductGallery>();
            Reviews = new HashSet<Review>();
            Tags = new HashSet<Tag>();
            WishLists = new HashSet<WishList>();
        }

        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public string ProductInStock { get; set; }
        public int ProductQuantityInStock { get; set; }
        public decimal ProductMainPrice { get; set; }
        public int GalleryId { get; set; }
        public double ProductScore { get; set; }
        public int TagId { get; set; }
        public int ReviewId { get; set; }
        public decimal? ProductOnSalePrice { get; set; }
        public int? SalePercent { get; set; }
        public int FeatureId { get; set; }
        public int CategoryId { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Feature> Features { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<ProductGallery> ProductGalleries { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
