using Microsoft.AspNetCore.Mvc;
using MyEshop;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using VenusDigital.Areas.Admin.Models;
using VenusDigital.Utilities;

namespace VenusDigital.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SendEmailController : Controller
    {
        #region Injection

        private IViewRenderService _viewRenderService;
        public INotyfService _notyfService;

        public SendEmailController(IViewRenderService viewRenderService, INotyfService notyfService)
        {
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

    }
}
