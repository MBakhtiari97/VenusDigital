using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyEshop;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using VenusDigital.Areas.Admin.Models;
using VenusDigital.Data;
using VenusDigital.Utilities;

namespace VenusDigital.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SendEmailController : Controller
    {
        #region Injection
        private VenusDigitalContext _context;
        private IViewRenderService _viewRenderService;
        public INotyfService _notyfService { get; }

        public SendEmailController(VenusDigitalContext context, IViewRenderService viewRenderService, INotyfService notyfService)
        {
            _context = context;
            _viewRenderService = viewRenderService;
            _notyfService = notyfService;
        }

        #endregion

        #region SendCustomEmail

        public IActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMail([Bind("Title,Description,Email")] SendEmailViewModel send)
        {
            if (!ModelState.IsValid)
                return View(send);
            //var body = await _viewRenderService.RenderToStringAsync("ManageEmails/_ActivateAccount";
            SendEmail.Send(send.Email, send.Title, send.Description);
            _notyfService.Success("Email Has Been Sent !");
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region SendNewsletter

        public IActionResult SendNews()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendNews(SendNewslettersViewModel news)
        {
            var emails = _context.Newsletters
                .Select(n => n.NewslettersSubedUserEmail)
                .ToList();

            foreach (var sendEmail in emails)
            {
                SendEmail.Send(sendEmail, news.Title, news.Description);
            }

            _notyfService.Information("Newsletter Email Has Been Sent To All Subscribed User's !");
            return RedirectToAction("Index", "Home");
        }
        #endregion

    }
}
