using System.Collections.Generic;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VenusDigital.Data.Repositories;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Controllers
{
    public class WishlistController : Controller
    {
        private IWishlistRepository _wishlistRepository;

        public WishlistController(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        #region Wishlist
        [Route("Wishlist")]
        public IActionResult WishList()
        {

            List<WishlistViewModel> wishlist = new List<WishlistViewModel>()
            {
                new WishlistViewModel()
                {
                ProductImage = "1.jpg",
                ProductMainPrice = 1300,
                ProductName = "Macbook Pro",
                ProductOffPrice = 1239,
                ReviewsCount = 89,
                ProductScore = 4.2f
                }
            };
            return View(wishlist);
        }

        #endregion
    }
}
