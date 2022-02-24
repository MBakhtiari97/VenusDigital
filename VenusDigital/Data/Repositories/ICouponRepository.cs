using System.Collections.Generic;
using System.Linq;
using VenusDigital.Models;

namespace VenusDigital.Data.Repositories
{
    public interface ICouponRepository
    {
        IEnumerable<Coupons> GetCoupons();
    }

    public class CouponRepository : ICouponRepository
    {
        private VenusDigitalContext _context;

        public CouponRepository(VenusDigitalContext context)
        {
            _context = context;
        }

        public IEnumerable<Coupons> GetCoupons()
        {
            return _context.Coupons
                .ToList();
        }
    }
}
