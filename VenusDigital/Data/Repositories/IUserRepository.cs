using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreHero.ToastNotification.Abstractions;
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
        void UpdatePassword(ChangePasswordViewModel password);
        void UpdateInformations(ChangeInfoViewModel info, int userId);
        ChangeInfoViewModel GetChangeInfo(int userId);
        void ActiveAccount(string identifierCode, string email);
        void InsertPostalInformation(PostalInformations info);
        PostalInformations userBillingAddress(int userId);
    }


    public class UserRepository : IUserRepository
    {
        private VenusDigitalContext _context;
        public INotyfService _notyfService { get; }

        public UserRepository(VenusDigitalContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
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
                .FirstOrDefault(p => p.UserId == userId);
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

        public void UpdatePassword(ChangePasswordViewModel password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.EmailAddress == password.Email && u.Password == password.CurrentPassword);

            if (user != null)
            {
                user.Password = password.Password;
                _notyfService.Success("Your Password Successfully Updated !");
                _context.SaveChanges();

            }
            else
            {
                _notyfService.Error("Invalid Credentials Please Check Fields !");
            }

        }

        public void UpdateInformations(ChangeInfoViewModel info, int userId)
        {
            var user = _context.Users
                .Include(u => u.PostalInformations)
                .FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                user.EmailAddress = info.Email;
                user.PhoneNumber = info.PhoneNumber;
                user.PostalInformations.First().Address = info.Address;
                user.PostalInformations.First().TelephoneNumber = info.TelephoneNumber;
                user.PostalInformations.First().ZipCode = info.ZipCode;
                user.UserIdentifierCode = Guid.NewGuid().ToString();
                _notyfService.Success("Your Information's Has Successfully Updated !");
                _context.SaveChanges();
                
            }
            else
            {
                _notyfService.Error("Cannot Verify Your Identity , Please Login Again And Then Try !");
            }
        }

        public ChangeInfoViewModel GetChangeInfo(int userId)
        {
            var userInfos = _context.Users
                .Include(u => u.PostalInformations)
                .FirstOrDefault(u => u.UserId == userId);

            ChangeInfoViewModel info = new ChangeInfoViewModel()
            {
                ZipCode = userInfos.PostalInformations.First().ZipCode,
                TelephoneNumber = userInfos.PostalInformations.First().TelephoneNumber,
                Address = userInfos.PostalInformations.First().Address,
                Email = userInfos.EmailAddress,
                PhoneNumber = userInfos.PhoneNumber
            };

            return info;
        }

        public void ActiveAccount(string identifierCode,string email)
        {
            if (_context.Users.Any(u => 
                    u.EmailAddress == email && u.UserIdentifierCode == identifierCode))
            {
                var user = _context.Users
                    .First(u => 
                        u.EmailAddress == email && u.UserIdentifierCode == identifierCode);

                user.IsActive = true;
                user.UserIdentifierCode = Guid.NewGuid().ToString();
                _context.SaveChanges();
                _notyfService.Success("Your Account Is Active Now !");
            }
            else
            {
                _notyfService.Error("Cannot Find Any User By This Credentials");
            }
        }

        public void InsertPostalInformation(PostalInformations info)
        {
            if (_context.PostalInformations.Any(b => b.UserId == info.UserId))
            {
                var billingAddress = _context
                    .PostalInformations
                    .FirstOrDefault(b => b.UserId == info.UserId);
                billingAddress.Address = info.Address;
                billingAddress.TelephoneNumber = info.TelephoneNumber;
                billingAddress.ZipCode = info.ZipCode;
                _context.SaveChanges();
            }
            else
            {
                _context.PostalInformations.Add(info);
                _context.SaveChanges();
            }
           
            _notyfService.Success("Your Billing Address Has Been Updated Successfully !");
        }

        public PostalInformations userBillingAddress(int userId)
        {
            return _context
                .PostalInformations
                .FirstOrDefault(b => b.UserId == userId);
        }
    }
}
