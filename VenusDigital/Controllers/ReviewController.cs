using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Controllers
{
    public class ReviewController : Controller
    {
        private IReviewsRepository _reviewsRepository;
        public INotyfService _notyfService { get; }

        public ReviewController(IReviewsRepository reviewsRepository,INotyfService notyfService)
        {
            _reviewsRepository = reviewsRepository;
            _notyfService = notyfService;
        }

        public IActionResult AddReview(SingleReviewViewModel review,int productId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            _reviewsRepository.AddReview(review,userId,productId);
            _notyfService.Success("Your Review Has Successfully Published !");

            return Redirect($"/Product-{productId}");
        }
    }
}
