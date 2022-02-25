using Microsoft.AspNetCore.Mvc;

namespace VenusDigital.Controllers
{
    public class CompareController : Controller
    {
        public IActionResult ShowCompareList()
        {
            return View();
        }
    }
}
