using System.Linq;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data;

namespace VenusDigital.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private VenusDigitalContext _context;

        public HomeController(VenusDigitalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Users = _context.Users
                .OrderByDescending(u => u.RegisterDate)
                .Take(9)
                .ToList();
            ViewBag.Reviews = _context.Reviews
                .Where(r => !r.IsPublished)
                .Take(9)
                .ToList();
            ViewBag.Orders = _context.Order
                .Where(o => !o.IsProcessed && o.IsFinally)
                .ToList();
            ViewBag.SupportTickets = _context.Supports
                .Where(s => !s.IsAnswered)
                .Take(9)
                .ToList();

            return View();
        }
    }
}
