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

        public ProductsController(IProductsRepository productsRepository
            , IReviewsRepository reviewsRepository
            , ICategoryRepository categoryRepository)
        {
            _productsRepository = productsRepository;
            _reviewsRepository = reviewsRepository;
            _categoryRepository = categoryRepository;
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
            return View(Product);
        }


        #endregion

        #region Search
        [Route("Search")]
        public IActionResult Search(string q)
        {
            List<SingleProductViewModel> ResultProduct = new List<SingleProductViewModel>();
            ResultProduct.AddRange(_productsRepository.GetProductByString(q));
            ViewBag.Count=ResultProduct.Count;
            ViewBag.Search = q.ToUpper();
            return View(ResultProduct.Distinct());
        }

        #endregion

        #region Products
        [Route("Category-{categoryId}")]
        public IActionResult ShowProductsByCategory(int categoryId)
        {
            List<Products> productsByCategory = new List<Products>();
            foreach (var productId in _categoryRepository.GetProductsByCategory(categoryId))
            {
                productsByCategory.Add(_productsRepository.GetProduct(productId));
            }

            //ViewBag.CategoryName = _categoryRepository.GetCategoryName(categoryId);
            return View(productsByCategory);
        }


        #endregion
    }
}
