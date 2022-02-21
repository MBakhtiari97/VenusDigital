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
                .Where(p=>p.ProductQuantityInStock!=0)
                .Include(p=>p.ProductGalleries)
                .Take(9)
                .ToList();
        }

        public Products GetProduct(int productId)
        {
            return _context.Products
                .Include(p=>p.ProductGalleries)
                .First(p => p.ProductId == productId);
        }

        public IEnumerable<SingleProductViewModel> GetProductByString(string q)
        {
            return _context.Products
                .Include(p=>p.ProductGalleries)
                .Where(p =>
                p.ProductTitle.Contains(q) || p.ProductShortDescription.Contains(q) ||
                p.ProductLongDescription.Contains(q))
                .Select(p=> new SingleProductViewModel()
                {
                    MainImage = p.ProductGalleries.First().ImageName,
                    MainPrice = p.ProductMainPrice,
                    ProductId = p.ProductId,
                    Quantiny = p.ProductQuantityInStock,
                    Score = p.ProductScore,
                    Title = p.ProductTitle
                }).ToList();
        }

        public List<string> GetProductTags(int productId)
        {
            return _context.Tags
                .Where(t => t.ProductId == productId)
                .Select(t => t.Tag)
                .ToList();
        }
    }
}
