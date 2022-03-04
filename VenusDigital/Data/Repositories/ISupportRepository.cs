using VenusDigital.Models;

namespace VenusDigital.Data.Repositories
{
    public interface ISupportRepository
    {
        void InsertTicket(Supports ticket);
    }

    public class SupportRepository : ISupportRepository
    {
        private VenusDigitalContext _context;

        public SupportRepository(VenusDigitalContext context)
        {
            _context = context;
        }

        public void InsertTicket(Supports ticket)
        {
            _context.Supports.Add(ticket);
            _context.SaveChanges();
        }
    }
}
