using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
            //First get all product id's that exist in categoryId 11
            var productIds = _context.SelectedCategory
                .Where(c => c.CategoryId == 11)
                .Select(c => c.ProductId)
                .ToList();

            //Then collecting all products by product id's
            List<Products> listProducts = new List<Products>();
            foreach (var productId in productIds)
            {
                listProducts.Add(_context.Products
                    .Include(p => p.ProductGalleries)
                    .FirstOrDefault(p => p.ProductId == productId));
            }

            //Finally filling a list of single product view model
            List<SingleProductViewModel> newHardware = new List<SingleProductViewModel>();
            foreach (var item in listProducts.OrderByDescending(p=>p.CreateDate).Take(4))
            {
                newHardware.Add(new SingleProductViewModel()
                {
                    MainImage = item.ProductGalleries.First().ImageName,
                    MainPrice = item.ProductMainPrice,
                    ProductId = item.ProductId,
                    Quantiny = item.ProductQuantityInStock,
                    Score = item.ProductScore,
                    Title = item.ProductTitle,
                    OnSalePrice = item.ProductOnSalePrice
                });
            }
            return newHardware;
        }

        public IEnumerable<SingleProductViewModel> GetNewPcAccessoriesProducts()
        {

            var productIds = _context.SelectedCategory
                .Where(c => c.CategoryId == 104)
                .Select(c => c.ProductId)
                .ToList();
            List<Products> listProducts = new List<Products>();
            foreach (var productId in productIds)
            {
                listProducts.Add(_context.Products
                    .Include(p => p.ProductGalleries)
                    .FirstOrDefault(p => p.ProductId == productId));
            }


            List<SingleProductViewModel> newAccessories = new List<SingleProductViewModel>();
            foreach (var item in listProducts.OrderByDescending(p => p.CreateDate).Take(4))
            {
                newAccessories.Add(new SingleProductViewModel()
                {
                    MainImage = item.ProductGalleries.First().ImageName,
                    MainPrice = item.ProductMainPrice,
                    ProductId = item.ProductId,
                    Quantiny = item.ProductQuantityInStock,
                    Score = item.ProductScore,
                    Title = item.ProductTitle,
                    OnSalePrice = item.ProductOnSalePrice
                });
            }
            return newAccessories;
        }

        public IEnumerable<SingleProductViewModel> GetNewPhonesProducts()
        {
            //var products = _context.SelectedCategory
            //    .Where(c => c.CategoryId == 2)
            //    .Select(c => c.Products)
            //    .OrderByDescending(c=>c.CreateDate)
            //    .Take(4)
            //    .ToList();

            var productIds = _context.SelectedCategory
                .Where(c => c.CategoryId == 2)
                .Select(c => c.ProductId)
                .ToList();
            List<Products> listProducts = new List<Products>();
            foreach (var productId in productIds)
            {
                listProducts.Add(_context.Products
                    .Include(p=>p.ProductGalleries)
                    .FirstOrDefault(p=>p.ProductId==productId));
            }


            List<SingleProductViewModel> newPhones = new List<SingleProductViewModel>();
            foreach (var item in listProducts.OrderByDescending(p => p.CreateDate).Take(4))
            {
                newPhones.Add(new SingleProductViewModel()
                {
                    MainImage = item.ProductGalleries.First().ImageName,
                    MainPrice = item.ProductMainPrice,
                    ProductId = item.ProductId,
                    Quantiny = item.ProductQuantityInStock,
                    Score = item.ProductScore,
                    Title = item.ProductTitle,
                    OnSalePrice = item.ProductOnSalePrice
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
                    Title = p.ProductTitle,
                    OnSalePrice = p.ProductOnSalePrice
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
                    ProductId = p.ProductId,
                    OnSalePrice = p.ProductOnSalePrice
                }).Take(3).ToList();
        }
    }
}
