using System;
using System.Collections.Generic;

#nullable disable

namespace VenusDigital.Models
{
    public partial class Discount
    {
        public int DiscountId { get; set; }
        public int? DiscountPercent { get; set; }
        public decimal? DiscountValue { get; set; }
        public string DiscountCode { get; set; }
        public int DiscountCodeCount { get; set; }
    }
}
