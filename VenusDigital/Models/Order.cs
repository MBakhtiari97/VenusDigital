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
        public bool IsFinally { get; set; }
        [Required]
        public decimal TotalOrderPrice { get; set; }
        public decimal? TotalPriceWithCoupon { get; set; }
        public bool AppliedCoupon { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentTraceCode { get; set; }
        
        //Nav
        [ForeignKey("UserId")]
        public Users Users { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
    }
}
