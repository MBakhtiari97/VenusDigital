using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenusDigital.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        [Display(Name = "Paid")]
        public bool IsFinally { get; set; }
        [Required]
        [Display(Name = "Order Price($)")]
        public decimal TotalOrderPrice { get; set; }
        [Display(Name = "Order Price (Applied Coupon)($)")]
        public decimal? TotalPriceWithCoupon { get; set; }
        [Display(Name = "Applied Coupon")]
        public bool AppliedCoupon { get; set; }
        [Display(Name = "Payment Date")]
        public DateTime? PaymentDate { get; set; }
        [Display(Name = "Trace Code")]
        public string? PaymentTraceCode { get; set; }
        [Display(Name = "Processed")]
        public bool IsProcessed { get; set; }
        [Display(Name = "Delivered")]
        public bool IsDelivered { get; set; }

        //Nav
        [ForeignKey("UserId")]
        public Users Users { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }

    }
}
