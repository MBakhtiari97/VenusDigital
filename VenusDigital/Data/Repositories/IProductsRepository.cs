﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VenusDigital.Models;

namespace VenusDigital.Data.Repositories
{
    public interface IProductsRepository
    {
        Products GetProduct(int productId);
        List<string> GetProductTags(int productId);
    }

    public class ProductsRepository : IProductsRepository
    {
        private VenusDigitalContext _context;

        public ProductsRepository(VenusDigitalContext context)
        {
            _context = context;
        }

        public Products GetProduct(int productId)
        {
            return _context.Products
                .Include(p=>p.ProductGalleries)
                .First(p => p.ProductId == productId);
        }

        public List<string> GetProductTags(int productId)
        {
            return _context.Tags.Where(t => t.ProductId == productId).Select(t => t.Tag).ToList();
        }
    }
}