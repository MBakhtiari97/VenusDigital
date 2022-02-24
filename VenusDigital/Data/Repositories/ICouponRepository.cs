namespace VenusDigital.Data.Repositories
{
    public interface ICouponRepository
    {

    }

    public class CouponRepository : ICouponRepository
    {
        private VenusDigitalContext _context;

        public CouponRepository(VenusDigitalContext context)
        {
            _context = context;
        }

    }
}
