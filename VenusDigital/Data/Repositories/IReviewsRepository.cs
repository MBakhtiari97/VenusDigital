using System.Linq;
using VenusDigital.Models;

namespace VenusDigital.Data.Repositories
{
    public interface IReviewsRepository
    {
        int GetTotalReviewsCount(int productId);
        bool AddReview(Reviews review);
    }

    public class ReviewRepository:IReviewsRepository
    {
        private VenusDigitalContext _context;

        public ReviewRepository(VenusDigitalContext context)
        {
            _context = context;
        }
        public int GetTotalReviewsCount(int productId)
        {
            return _context.Reviews
                .Count(r => r.ProductId == productId);
        }

        public bool AddReview(Reviews review)
        {
            throw new System.NotImplementedException();
        }
    }
}
