using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;

namespace VenusDigital.Controllers
{
    [Authorize]
    public class CompareController : Controller
    {
        #region InjectRepository

        private ICompareRepository _compareRepository;
        public INotyfService _notyfServic { get; }

        public CompareController(ICompareRepository compareRepository, INotyfService notyfService)
        {
            _compareRepository = compareRepository;
            _notyfServic = notyfService;
        }


        #endregion
        [Route("Compare")]
        public IActionResult ShowCompareList()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());

            return View(_compareRepository.GetCompareItems(userId));
        }

        #region CompareOperations

        public IActionResult AddToCompare(int productId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            _compareRepository.AddToCompare(productId, userId);
            _notyfServic.Success("Item Successfully Added To Compare List!");
            return RedirectToAction("ShowCompareList");
        }

        public IActionResult RemoveFromCompare(int compareId)
        {
            var compareItem = _compareRepository.getCompareById(compareId);
            _compareRepository.RemoveFromCompare(compareItem);
            _notyfServic.Success("Item Successfully Removed From Your Compare List!");
            return RedirectToAction("ShowCompareList");
        }

        #endregion

    }
}
