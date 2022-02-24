using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VenusDigital.Models;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Data.Repositories
{
    public interface IUserRepository
    {
        bool IsExistedUserByEmail(string email);
        void AddUser(Users user);
        Users GetUserForLogin(string email, string password);
        Users RecoverPasswordByIdentifier(string identifierCode);
        Users GetUserByEmail(string email);
        void SaveChanges();
        Users GetUserByUserId(int userId);
        PostalInformations GetPostalInformation(int userId);
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

        public Users GetUserByEmail(string email)
        {
            return _context.Users
                .First(u => u.EmailAddress == email);
        }

        public Users GetUserForLogin(string email, string password)
        {
            return _context.Users
                .SingleOrDefault(u => u.EmailAddress == email && u.Password == password);
        }

        public Users GetUserByUserId(int userId)
        {
            
            return _context.Users
                .Find(userId);
        }

        public bool IsExistedUserByEmail(string email)
        {
            return _context.Users
                .Any(u => u.EmailAddress == email);
        }

        public PostalInformations GetPostalInformation(int userId)
        {
            return _context.PostalInformations
                .FirstOrDefault(p=>p.UserId==userId);
        }

        public Users RecoverPasswordByIdentifier(string identifierCode)
        {
            if (_context.Users.Any(u =>
                    u.UserIdentifierCode == identifierCode))
            {
                return _context.Users.Single(u =>
                    u.UserIdentifierCode == identifierCode);
            }

            return null;

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
