using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VenusDigital.Models;

namespace VenusDigital.Data.Repositories
{
    public interface ICompareRepository
    {
        IEnumerable<Compare> GetCompareItems(int userId);
        void AddToCompare(int productId, int userId);
        void RemoveFromCompare(Compare compareItem);
        Compare getCompareById(int compareId);

    }

    public class CompareRepository : ICompareRepository
    {
        private VenusDigitalContext _context;

        public CompareRepository(VenusDigitalContext context)
        {
            _context = context;
        }

        public void AddToCompare(int productId,int userId)
        {
            Compare compareItem = new Compare()
            {
                ProductId = productId,
                UserId = userId
            };
            _context.Compare.Add(compareItem);
            _context.SaveChanges();
        }

        public Compare getCompareById(int compareId)
        {
            return _context.Compare
                .Find(compareId);
        }

        public IEnumerable<Compare> GetCompareItems(int userId)
        {
            return _context.Compare
                .Include(c=>c.Products)
                .ThenInclude(c=>c.ProductGalleries)
                .Where(c => c.UserId == userId)
                .ToList();
        }

        public void RemoveFromCompare(Compare compareItem)
        {
            _context.Compare.Remove(compareItem);
            _context.SaveChanges();
        }
    }
}
