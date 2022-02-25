using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;

namespace VenusDigital.Controllers
{
    public class ReviewController : Controller
    {
        private IReviewsRepository _reviewsRepository;

        public ReviewController(IReviewsRepository reviewsRepository)
        {
            _reviewsRepository = reviewsRepository;
        }


    }
}
