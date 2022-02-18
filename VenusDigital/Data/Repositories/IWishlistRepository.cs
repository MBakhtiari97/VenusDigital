using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VenusDigital.Data.Repositories
{
    public interface IWishlistRepository
    {
        IEnumerable<int> GetAllWishlistProductsForUser(int userId);
    }

    public class WishlistRepository:IWishlistRepository
    {
        private VenusDigitalContext _contexct;

        public WishlistRepository(VenusDigitalContext contexct)
        {
            _contexct = contexct;
        }

        public IEnumerable<int> GetAllWishlistProductsForUser(int userId)
        {
            var allProductIds = _contexct.WishLists
                .Where(w => w.UserId == userId)
                .Select(w => w.ProductId)
                .ToList();

            return allProductIds;
        }

       
    }
}
