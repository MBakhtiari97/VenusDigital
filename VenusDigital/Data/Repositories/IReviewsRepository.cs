using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using VenusDigital.Models;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Data.Repositories
{
    public interface IReviewsRepository
    {
        int GetTotalReviewsCount(int productId);
        void AddReview(SingleReviewViewModel review, int userId, int productId);
        IEnumerable<Reviews> GetAllReviews();
        IEnumerable<SingleReviewViewModel> GetReviewsForProduct(int productId);

    }

    public class ReviewRepository : IReviewsRepository
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

        public IEnumerable<Reviews> GetAllReviews()
        {
            return _context.Reviews
                .ToList();
        }

        public IEnumerable<SingleReviewViewModel> GetReviewsForProduct(int productId)
        {
            return _context.Reviews
                .Where(r => r.ProductId == productId && r.IsPublished).Select(r => new SingleReviewViewModel()
                {
                    Username = r.Users.UserName,
                    ReviewTitle = r.ReviewTitle,
                    ReviewDescription = r.ReviewDescription,
                    ReviewDate = r.ReviewCreateDate,
                    ReviewId = r.ReviewId,
                    ReviewScore = (int)r.ReviewScore
                }).ToList();
        }

        public void AddReview(SingleReviewViewModel review, int userId, int productId)
        {

            Reviews reviews = new Reviews()
            {
                ProductId = productId,
                ReviewCreateDate = DateTime.Now,
                ReviewScore = review.ReviewScore,
                ReviewTitle = review.ReviewTitle,
                UserId = userId,
                ReviewDescription = review.ReviewDescription,
                IsPublished = false
            };

            _context.Reviews.Add(reviews);
            _context.SaveChanges();

            var product = _context.Products.Find(productId);
            product.ProductScore = (float)Math.Round((product.ProductScore * product.Reviews.Count)
                                    + review.ReviewScore) / (product.Reviews.Count + 1);
            _context.SaveChanges();
        }
    }
}
