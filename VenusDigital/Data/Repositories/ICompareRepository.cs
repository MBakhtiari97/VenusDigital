namespace VenusDigital.Data.Repositories
{
    public interface ICompareRepository
    {
    }

    public class CompareRepository : ICompareRepository
    {
        private VenusDigitalContext _context;

        public CompareRepository(VenusDigitalContext context)
        {
            _context = context;
        }

    }
}
