using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VenusDigital.Models;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Data.Repositories
{
    public interface IProductsRepository
    {
        Products GetProduct(int productId);
        List<string> GetProductTags(int productId);
        IEnumerable<Products> GetNewProducts();
        IEnumerable<SingleProductViewModel> GetProductByString(string q);
        IEnumerable<Products> GetOnSaleProducts();
        IEnumerable<SpecialOffersViewModel> GetSpecialOffers();
        IEnumerable<Products> GetProductsByPriceFilter(decimal min, decimal max, int categoryId);
        Products GetProductForCart(int productId);
        IEnumerable<SingleProductViewModel> GetNewPhonesProducts();
        IEnumerable<SingleProductViewModel> GetNewHardwareProducts();
        IEnumerable<SingleProductViewModel> GetNewPcAccessoriesProducts();
    }

    public class ProductsRepository : IProductsRepository
    {
        private VenusDigitalContext _context;

        public ProductsRepository(VenusDigitalContext context)
        {
            _context = context;
        }

        public IEnumerable<SingleProductViewModel> GetNewHardwareProducts()
        {
            var hardwareProducts = _context.SelectedCategory
                .Where(c => c.CategoryId == 11)
                .Select(c => c.Products)
                .OrderByDescending(c => c.CreateDate)
                .Take(4)
                .ToList();

            List<SingleProductViewModel> newHardware = new List<SingleProductViewModel>();
            foreach (var product in hardwareProducts)
            {
                newHardware.Add(new SingleProductViewModel()
                {
                    MainImage = product.ProductGalleries.First().ImageName,
                    MainPrice = product.ProductMainPrice,
                    ProductId = product.ProductId,
                    Quantiny = product.ProductQuantityInStock,
                    Score = product.ProductScore,
                    Title = product.ProductTitle
                });
            }

            return newHardware;
        }

        public IEnumerable<SingleProductViewModel> GetNewPcAccessoriesProducts()
        {
            var PcProducts = _context.SelectedCategory
                .Where(c => c.CategoryId == 104)
                .Select(c => c.Products)
                .OrderByDescending(c => c.CreateDate)
                .Take(4)
                .ToList();

            List<SingleProductViewModel> newAccessories = new List<SingleProductViewModel>();
            foreach (var product in PcProducts)
            {
                newAccessories.Add(new SingleProductViewModel()
                {
                    MainImage = product.ProductGalleries.First().ImageName,
                    MainPrice = product.ProductMainPrice,
                    ProductId = product.ProductId,
                    Quantiny = product.ProductQuantityInStock,
                    Score = product.ProductScore,
                    Title = product.ProductTitle
                });
            }

            return newAccessories;
        }

        public IEnumerable<SingleProductViewModel> GetNewPhonesProducts()
        {
            var products = _context.SelectedCategory
                .Where(c => c.CategoryId == 2)
                .Select(c => c.Products)
                .OrderByDescending(c=>c.CreateDate)
                .Take(4)
                .ToList();

            List<SingleProductViewModel> newPhones = new List<SingleProductViewModel>();
            foreach (var product in products)
            {
                newPhones.Add(new SingleProductViewModel()
                {
                    MainImage = product.ProductGalleries.First().ImageName,
                    MainPrice = product.ProductMainPrice,
                    ProductId = product.ProductId,
                    Quantiny = product.ProductQuantityInStock,
                    Score = product.ProductScore,
                    Title = product.ProductTitle
                });
            }

            return newPhones;
        }

        public IEnumerable<Products> GetNewProducts()
        {
            return _context.Products
                .OrderByDescending(p => p.CreateDate)
                .Where(p => p.ProductQuantityInStock != 0)
                .Include(p => p.ProductGalleries)
                .Take(4)
                .ToList();
        }

        public IEnumerable<Products> GetOnSaleProducts()
        {
            return _context.Products
                .Include(p => p.ProductGalleries)
                .Where(p => p.ProductOnSalePrice != 0)
                .OrderByDescending(p => p.CreateDate)
                .ToList();
        }

        public Products GetProduct(int productId)
        {
            return _context.Products
                .Include(p => p.ProductGalleries)
                .First(p => p.ProductId == productId);
        }

        public IEnumerable<SingleProductViewModel> GetProductByString(string q)
        {
            return _context.Products
                .Include(p => p.ProductGalleries)
                .Where(p =>
                p.ProductTitle.Contains(q) || p.ProductShortDescription.Contains(q) ||
                p.ProductLongDescription.Contains(q))
                .Select(p => new SingleProductViewModel()
                {
                    MainImage = p.ProductGalleries.First().ImageName,
                    MainPrice = p.ProductMainPrice,
                    ProductId = p.ProductId,
                    Quantiny = p.ProductQuantityInStock,
                    Score = p.ProductScore,
                    Title = p.ProductTitle
                }).ToList();
        }

        public Products GetProductForCart(int productId)
        {
            return _context.Products
                .Find(productId);
        }

        public IEnumerable<Products> GetProductsByPriceFilter(decimal min, decimal max, int categoryId)
        {

            return _context.SelectedCategory
                .Where(c => c.CategoryId == categoryId)
                .Include(p => p.Products)
                .ThenInclude(p => p.ProductGalleries)
                .Select(c => c.Products)
                .Where(p => p.ProductMainPrice >= min && p.ProductMainPrice <= max)
                .ToList();
        }

        public List<string> GetProductTags(int productId)
        {
            return _context.Tags
                .Where(t => t.ProductId == productId)
                .Select(t => t.Tag)
                .ToList();
        }

        public IEnumerable<SpecialOffersViewModel> GetSpecialOffers()
        {
            return _context.Products
                .Include(p => p.ProductGalleries)
                .Where(p => p.ProductOnSalePrice != 0)
                .OrderByDescending(p => p.CreateDate)
                .Select(p => new SpecialOffersViewModel()
                {
                    ImageName = p.ProductGalleries.First().ImageName,
                    Price = p.ProductMainPrice,
                    ProductScore = p.ProductScore,
                    ProductTitle = p.ProductTitle,
                    ProductId = p.ProductId
                }).Take(3).ToList();
        }
    }
}
