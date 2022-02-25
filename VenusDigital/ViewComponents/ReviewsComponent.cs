using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;

namespace VenusDigital.ViewComponents
{
    public class ReviewsComponent:ViewComponent
    {
        private IReviewsRepository _reviewsRepository;

        public ReviewsComponent(IReviewsRepository reviewsRepository)
        {
            _reviewsRepository = reviewsRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            return View("/Views/ViewComponents/_Reviews.cshtml",_reviewsRepository.GetReviewsForProduct(productId));
        }
    }
}
