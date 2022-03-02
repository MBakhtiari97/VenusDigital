using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Models
{
    public class Coupons
    {
        [Key]
        public int CouponId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Coupon Code")]
        public string CouponCode { get; set; }
        [Display(Name = "Coupon Value(%)")]
        public int? CouponPercent { get; set; }
        [Display(Name = "Coupon Value($)")]
        public decimal? CouponValue { get; set; }
        [Display(Name = "Count")]
        [Required]
        public int CouponCodeCount { get; set; }
    }
}
