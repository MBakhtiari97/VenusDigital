using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Models.ViewModels
{
    public class ProductDetailsViewModel
    {
        public string MainImage { get; set; }
        public string Title { get; set; }
        public float Score { get; set; }
        public int ReviewsCount { get; set; }
        public string Availability { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public decimal MainPrice { get; set; }
        public decimal SalePrice { get; set; }
    }

    public class SingleProductViewModel
    {
        public int ProductId { get; set; }
        public string MainImage { get; set; }
        public string Title { get; set; }
        public float Score { get; set; }
        public decimal MainPrice { get; set; }
    }
}
