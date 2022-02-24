using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyEshop;
using VenusDigital.Data.Repositories;
using VenusDigital.Models;
using VenusDigital.Models.ViewModels;
using VenusDigital.Utilities;

namespace VenusDigital.Controllers
{
    public class AccountController : Controller
    {
        #region InjectionRepository

        private IUserRepository _userRepository;
        private IViewRenderService _viewRenderService;

        public AccountController(IUserRepository userRepository, IViewRenderService viewRenderService)
        {
            _userRepository = userRepository;
            _viewRenderService = viewRenderService;
        }

        #endregion

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
                RegisterDate = DateTime.Now,
                UserIdentifierCode = Guid.NewGuid().ToString()
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


            var user = _userRepository.GetUserForLogin(login.Email.ToLower(), login.Password);
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

        #region ForgetPassword
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPasswordAsync(ForgetPasswordViewModel forget)
        {
            if (!ModelState.IsValid)
            {
                return View(forget);
            }

            var user = _userRepository.GetUserByEmail(forget.Email);
            if (user != null)
            {
                var body = await _viewRenderService.RenderToStringAsync("ManageEmails/_RecoverPassword", user);
                SendEmail.Send(forget.Email, "Recover Password", body);
                return View("SuccesForgotPassword", user);

            }
            else
            {
                ModelState.AddModelError("Email", "Cannot find any user with this email address !");
                return View(forget);
            }
        }

        [Route("RecoverPassword/{id}")]
        public IActionResult RecoverPassword(string id)
        {
            var user = _userRepository.RecoverPasswordByIdentifier(id);
            if (user == null)
                return RedirectToAction("NotFound", "Home");

            return View();
        }
        [HttpPost]
        [Route("RecoverPassword/{id}")]
        public async Task<IActionResult> RecoverPassword(string id, RecoverPasswordViewModel recover)
        {
            if (!ModelState.IsValid)
            {
                return View(recover);
            }

            var user = _userRepository.RecoverPasswordByIdentifier(id);
            if (user != null)
            {
                user.Password = recover.Password;
                user.UserIdentifierCode = Guid.NewGuid().ToString();
                _userRepository.SaveChanges();

                var body = await _viewRenderService.RenderToStringAsync("ManageEmails/_RecoveryPasswordNotification", user);
                SendEmail.Send(user.EmailAddress, "Recover Password", body);

                return View("_SuccessRecovery");
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }
        #endregion

        #region MyAccount
        [Authorize]
        public IActionResult ShowUserInfo()
        {
            //Getting UserId
            int userId = int.Parse(User
                .FindFirstValue(ClaimTypes.NameIdentifier)
                .ToString());

            //Getting Additional Posting User Information's In ViewBag
            ViewBag.PostalInfo = _userRepository
                 .GetPostalInformation(userId);
            //Getting User
            var user = _userRepository
                .GetUserByUserId(userId);

            return View(user);
        }

        #endregion

    }
}
