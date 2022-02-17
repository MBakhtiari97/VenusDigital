using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using VenusDigital.Data.Repositories;
using VenusDigital.Models;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region RegisterUser

        [Route("/Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("/Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
                return View(register);
            if (_userRepository.IsExistedUserByEmail(register.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "This email address has taken already!");
                return View(register);
            }

            if (register.PhoneNumber == "")
                register.PhoneNumber = "0";
            //Hashing password
            var password = Encoding.ASCII
                .GetBytes(register.Password);

            var md5 = new MD5CryptoServiceProvider();

            var hashedPassword = md5
                .ComputeHash(password);


            Users user = new Users()
            {
                EmailAddress = register.Email.ToLower(),
                IsAdmin = false,
                Password = hashedPassword,
                PhoneNumber = register.PhoneNumber,
                UserName = register.UserName,
                RegisterDate = DateTime.Now
            };
            _userRepository.AddUser(user);

            return View("_SuccessRegistration", register.UserName);
        }

        #endregion

        #region LoginUser
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            return View();
        }

        #endregion

        //TODO:Complete Login Code
    }
}
