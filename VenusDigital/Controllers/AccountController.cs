using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
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

            Users user = new Users()
            {
                EmailAddress = register.Email.ToLower(),
                IsAdmin = false,
                Password = register.Password,
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
            if (!ModelState.IsValid)
                return View(login);


            var user = _userRepository.GetUserForLogin(login.Email.ToLower(),login.Password);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Cannot find any user with these credentials !");
                return View(login);
            }

            //Logging user codes
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.EmailAddress),
                new Claim("IsAdmin", user.IsAdmin.ToString())
            };

            var identifier = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identifier);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = login.RememberMe
            };

            HttpContext.SignInAsync(principal, properties);

            return Redirect("/");
        }

        #endregion

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        #endregion

    }
}
