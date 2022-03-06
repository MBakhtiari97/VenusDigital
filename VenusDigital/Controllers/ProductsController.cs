using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;
using VenusDigital.Models;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Controllers
{
    public class ProductsController : Controller
    {
        #region InjectionRepository

        private IProductsRepository _productsRepository;
        private IReviewsRepository _reviewsRepository;
        private ICategoryRepository _categoryRepository;
        private IFeaturesRepository _featuresRepository;

        public ProductsController(IProductsRepository productsRepository
            , IReviewsRepository reviewsRepository
            , ICategoryRepository categoryRepository
            , IFeaturesRepository featuresRepository)
        {
            _productsRepository = productsRepository;
            _reviewsRepository = reviewsRepository;
            _categoryRepository = categoryRepository;
            _featuresRepository = featuresRepository;
        }

        #endregion

        #region ShowProduct

        [Route("Product-{productId}")]
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
                Quantiny = product.ProductQuantityInStock,
                ProductId = product.ProductId
            };

            List<Categories> categories = new List<Categories>();
            foreach (var categoryId in _categoryRepository.GetSelectedCategories(productId))
            {
                categories.Add(_categoryRepository.GetCategoryByCategoryId(categoryId));
            }

            ViewBag.ProductCategories = categories;
            ViewBag.ImageGallery = product.ProductGalleries;
            ViewBag.Tags = _productsRepository.GetProductTags(productId);
            ViewBag.Features = _featuresRepository.GetAllFeaturesByProductId(productId);
            return View(Product);
        }


        #endregion

        #region Search
        [Route("Search")]
        public IActionResult Search(string q)
        {
            List<SingleProductViewModel> ResultProduct = new List<SingleProductViewModel>();
            ResultProduct.AddRange(_productsRepository.GetProductByString(q));
            ViewBag.Count = ResultProduct.Count;
            ViewBag.Search = q.ToUpper();
            return View(ResultProduct.Distinct());
        }

        #endregion

        #region ProductsInCategories
        [Route("Category-{categoryId}")]
        public IActionResult ShowProductsByCategory(int categoryId, int pageId = 1)
        {
            ViewBag.pageId = pageId;

            List<Products> productsByCategory = new List<Products>();

            foreach (var productId in _categoryRepository.GetProductsByCategory(categoryId))
            {
                productsByCategory.Add(_productsRepository.GetProduct(productId));
            }

            ViewBag.Banner = _categoryRepository.GetCategoryBannerName(categoryId);
            ViewBag.CategoryId = categoryId;
            //ViewBag.CategoryId = categoryId;
            //ViewBag.CategoryName = _categoryRepository.GetCategoryName(categoryId);


            //Paging
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = productsByCategory.Count() / take;
            return View(productsByCategory.Skip(skip).Take(take).ToList());
        }
        #endregion

        #region ShowOnSaleProducts

        [Route("ShowOnSaleProducts")]
        public IActionResult ShowOnSaleProducts()
        {
            return View("ShowProductsByCategory", _productsRepository.GetOnSaleProducts());
        }


        #endregion

        #region ShowBestSellingProducts

        [Route("BestSelling")]
        public IActionResult ShowBestSellingProducts()
        {
            return View("ShowProductsByCategory", _productsRepository.GetBestSellingProducts());
        }


        #endregion

        #region FilterProducts
        [Route("Filter")]
        public IActionResult FilterProductsByPrice(decimal min, decimal max, int categoryId)
        {
            //TODO:Fix Pagination
            return View("ShowProductsByCategory", _productsRepository.GetProductsByPriceFilter(min, max, categoryId));
        }
        #endregion

    }
}
