using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsRepository _productsRepository;
        private IReviewsRepository _reviewsRepository;

        public ProductsController(IProductsRepository productsRepository,IReviewsRepository reviewsRepository)
        {
            _productsRepository = productsRepository;
            _reviewsRepository = reviewsRepository;
        }
        [Route("{productId}")]
        public IActionResult ShowProductDetails(int productId)
        {
            var product = _productsRepository.GetProduct(productId);
            var reviewCount = _reviewsRepository.GetTotalReviewsCount(productId);

            var Product = new ProductDetailsViewModel()
            {
                ReviewsCount = reviewCount,
                Availability = product.ProductInStock,
                FullDescription = product.ProductLongDescription,
                MainPrice = product.ProductMainPrice,
                SalePrice = product.ProductOnSalePrice,
                Score = product.ProductScore,
                ShortDescription = product.ProductShortDescription,
                Title = product.ProductTitle,
                MainImage = product.ProductGalleries.First().ImageName,
                Quantiny = product.ProductQuantityInStock
            };

            ViewBag.ImageGallery = product.ProductGalleries;
            ViewBag.Tags = _productsRepository.GetProductTags(productId);
            return View(Product);
        }
    }
}
