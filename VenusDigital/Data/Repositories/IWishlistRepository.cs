using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VenusDigital.Models;

namespace VenusDigital.Data.Repositories
{
    public interface IWishlistRepository
    {
        IEnumerable<int> GetAllWishlistProductsForUser(int userId);
        void AddToWishlist(int userId, int productId);
        void RemoveFromWishlist(int userId, int productId);
    }

    public class WishlistRepository:IWishlistRepository
    {
        private VenusDigitalContext _contexct;

        public WishlistRepository(VenusDigitalContext contexct)
        {
            _contexct = contexct;
        }

        public void AddToWishlist(int userId, int productId)
        {
            if (!_contexct.WishLists.Any(w => w.UserId == userId && w.ProductId == productId))
            {
                _contexct.WishLists.Add(new WishLists()
                {
                    ProductId = productId,
                    UserId = userId
                });
                _contexct.SaveChanges();
            }
        }

        public IEnumerable<int> GetAllWishlistProductsForUser(int userId)
        {
            var allProductIds = _contexct.WishLists
                .Where(w => w.UserId == userId)
                .Select(w => w.ProductId)
                .ToList();

            return allProductIds;
        }

        public void RemoveFromWishlist(int userId, int productId)
        {
            var wishlist = _contexct.WishLists.FirstOrDefault(w => w.UserId == userId && w.ProductId == productId);
            if (wishlist != null)
            {
                _contexct.WishLists.Remove(wishlist);
                _contexct.SaveChanges();
            }
        }
    }
}
