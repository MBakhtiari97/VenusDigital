using System.ComponentModel.DataAnnotations;

namespace VenusDigital.Models
{
    public class Coupons
    {
        [Key]
        public int CouponId { get; set; }
        [Required]
        [MaxLength(50)]
        public string CouponCode { get; set; }
        public int CouponPercent { get; set; }
        public decimal CouponValue { get; set; }
        [Required]
        public int CouponCodeCount { get; set; }
    }
}
