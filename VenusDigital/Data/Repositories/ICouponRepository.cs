using System.Collections.Generic;
using System.Linq;
using VenusDigital.Models;

namespace VenusDigital.Data.Repositories
{
    public interface ICouponRepository
    {
        Coupons GetCoupons(string coupon);
    }

    public class CouponRepository : ICouponRepository
    {
        private VenusDigitalContext _context;

        public CouponRepository(VenusDigitalContext context)
        {
            _context = context;
        }

        public Coupons GetCoupons(string coupon)
        {
            return _context.Coupons
                .FirstOrDefault(c=>c.CouponCode==coupon);
        }
    }
}
