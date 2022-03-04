using System;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Models;
using Microsoft.AspNetCore.HttpOverrides;
using VenusDigital.Data.Repositories;

namespace VenusDigital.Controllers
{
    public class SupportController : Controller
    {
        private ISupportRepository _supportRepository;
        public INotyfService _notyfService;

        public SupportController(ISupportRepository supportRepository,INotyfService notyfService)
        {
            _supportRepository = supportRepository;
            _notyfService = notyfService;
        }

        [Route("HelpCenter")]
        public IActionResult HelpCenter()
        {
            return View();
        }
        [Route("Ticket")]
        public IActionResult Support()
        {
            return View();
        }
        [HttpPost]
        [Route("Ticket")]
        public IActionResult Support(Supports ticket)
        {
            if (!ModelState.IsValid)
                return View(ticket);
            //TODO:GO FOR NUGET PACKAGE BELOW:
            //Microsoft.AspNetCore.HttpOverridesIn Startup.cs, in the Configure() method add:

            ticket.UserIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            ticket.RequestCode = Guid.NewGuid().ToString();
            _supportRepository.InsertTicket(ticket);
            _notyfService.Success("Your Support Ticket Has Been Sent Successfully !");
            return RedirectToAction("HelpCenter");
        }
    }
}
