using Microsoft.AspNetCore.Mvc;

namespace VenusDigital.Controllers
{
    public class SupportController : Controller
    {
        [Route("HelpCenter")]
        public IActionResult HelpCenter()
        {
            return View();
        }
    }
}
