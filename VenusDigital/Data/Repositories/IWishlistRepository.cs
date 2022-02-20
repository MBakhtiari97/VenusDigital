using System.Collections.Generic;
using System.Linq;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public INotyfService _notifyService { get; }

        public WishlistRepository(VenusDigitalContext contexct, INotyfService notifyService)
        {
            _contexct = contexct;
            _notifyService = notifyService;
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
                _notifyService.Success("Item has successfully added to your Wishlist");
            }
            else
            {
                _notifyService.Error("This item Existed is in your Wishlist!");
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
                _notifyService.Success("Item has successfully removed from your wish list");
            }
        }
    }
}
