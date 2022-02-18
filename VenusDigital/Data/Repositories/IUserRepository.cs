using System.Linq;
using VenusDigital.Models;

namespace VenusDigital.Data.Repositories
{
    public interface IUserRepository
    {
        bool IsExistedUserByEmail(string email);
        void AddUser(Users user);
        Users GetUserForLogin(string email, string password);
    }


    public class UserRepository : IUserRepository
    {
        private VenusDigitalContext _context;

        public UserRepository(VenusDigitalContext context)
        {
            _context = context;
        }

        public void AddUser(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public Users GetUserForLogin(string email, string password)
        {
            return _context.Users
                .SingleOrDefault(u => u.EmailAddress == email && u.Password == password);
        }

        public bool IsExistedUserByEmail(string email)
        {
            return _context.Users.Any(u => u.EmailAddress == email);
        }
    }
}
