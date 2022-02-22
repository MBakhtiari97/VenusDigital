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
        IEnumerable<Products> GetProductsByPriceFilter(decimal min, decimal max);
    }

    public class ProductsRepository : IProductsRepository
    {
        private VenusDigitalContext _context;

        public ProductsRepository(VenusDigitalContext context)
        {
            _context = context;
        }

        public IEnumerable<Products> GetNewProducts()
        {
            return _context.Products
                .OrderByDescending(p => p.CreateDate)
                .Where(p => p.ProductQuantityInStock != 0)
                .Include(p => p.ProductGalleries)
                .Take(9)
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

        public IEnumerable<Products> GetProductsByPriceFilter(decimal min, decimal max)
        {
            return _context.Products
                .Include(p => p.ProductGalleries)
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
