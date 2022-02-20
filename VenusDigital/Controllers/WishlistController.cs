using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VenusDigital.Data.Repositories;
using VenusDigital.Models;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private IWishlistRepository _wishlistRepository;
        private IProductsRepository _productsRepository;
        private IReviewsRepository _reviewsRepository;
        

        public WishlistController(IWishlistRepository wishlistRepository
            , IProductsRepository productsRepository
            , IReviewsRepository reviewsRepository
            )
        {
            _wishlistRepository = wishlistRepository;
            _productsRepository = productsRepository;
            _reviewsRepository = reviewsRepository;
            
        }

        #region Wishlist
        [Route("Wishlist")]
        public IActionResult WishList()
        {
            //Retrieving all product id's
            var productIds =
                _wishlistRepository.GetAllWishlistProductsForUser(
                    int.Parse
                        (User.FindFirstValue(ClaimTypes.NameIdentifier)
                            .ToString()));

            //Creating a list of wishlist view model

            var wishList = new List<WishlistViewModel>();

            //Required Variables
            Products product;
            int reviewCount;

            //Filling variables and viewmodel
            foreach (var productId in productIds)
            {
                product = _productsRepository.GetProduct(productId);
                reviewCount = _reviewsRepository.GetTotalReviewsCount(productId);


                wishList.Add(new WishlistViewModel
                {
                    ProductImage = product.ProductGalleries.First().ImageName,
                    ProductMainPrice = product.ProductMainPrice,
                    ProductName = product.ProductTitle,
                    ProductOffPrice = product.ProductOnSalePrice,
                    ProductScore = product.ProductScore,
                    ReviewsCount = reviewCount,
                    QuantityInStock = product.ProductQuantityInStock,
                    ProductId = product.ProductId
                }
                );

            }
            return View(wishList);
        }

        #endregion

        #region WishlistOperations
        public IActionResult AddToWishlist(int productId)
        {
            _wishlistRepository.AddToWishlist(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()),productId);
            return Redirect($"/Product-{productId}");
        }

        public IActionResult RemoveFromWishlist(int productId)
        {
            _wishlistRepository.RemoveFromWishlist(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()), productId);
            return RedirectToAction("WishList");
        }

        #endregion
    }
}
