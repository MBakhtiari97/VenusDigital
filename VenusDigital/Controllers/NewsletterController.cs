using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;
using VenusDigital.Models;

namespace VenusDigital.Controllers
{
    public class NewsletterController : Controller
    {
        private INewsLetterRepository _newsletterRepository;
        public INotyfService _notifyService { get; }

        public NewsletterController(INewsLetterRepository newsletterRepository, INotyfService notifyService)
        {
            _newsletterRepository = newsletterRepository;
            _notifyService = notifyService;
        }

        [Route("Newsletter")]
        public IActionResult AddUserToNewsLetter()
        {
            return View();
        }
        [HttpPost]
        [Route("Newsletter")]
        public IActionResult AddUserToNewsLetter(Newsletters newsletter)
        {
            if (!ModelState.IsValid)
            {
                return View(newsletter);
            }

            if (!_newsletterRepository.IsExistedByEmail(newsletter.NewslettersSubedUserEmail))
            {
                _newsletterRepository.AddUserToNewsLetterService(newsletter.NewslettersSubedUserEmail);
                _notifyService.Success("Congratulations ! Your Subscription has successfully completed!");
                return Redirect("/");
            }
            _notifyService.Success("You've Subscribed already !");
            return Redirect("/");
        }
    }
}
