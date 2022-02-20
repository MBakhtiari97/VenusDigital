using System.Linq;
using VenusDigital.Models;

namespace VenusDigital.Data.Repositories
{
    public interface INewsLetterRepository
    {
        bool IsExistedByEmail(string email);
        void AddUserToNewsLetterService(string email);
    }

    public class NewsLetterRepository : INewsLetterRepository
    {
        VenusDigitalContext _context;

        public NewsLetterRepository(VenusDigitalContext context)
        {
            _context = context;
        }
        public void AddUserToNewsLetterService(string email)
        {

            _context.Newsletters.Add(new Newsletters()
            {
                NewslettersSubedUserEmail = email
            });
            _context.SaveChanges();

        }

        public bool IsExistedByEmail(string email)
        {
            return _context.Newsletters.Any(n => n.NewslettersSubedUserEmail == email);
        }
    }
}
