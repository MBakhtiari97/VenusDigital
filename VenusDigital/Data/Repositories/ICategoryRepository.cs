using System.Collections.Generic;
using System.Linq;
using VenusDigital.Models;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Data.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Categories> GetCategories();
        IEnumerable<int> GetSelectedCategories(int productId);
        Categories GetCategoryByCategoryId(int categoryId);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private VenusDigitalContext _context;

        public CategoryRepository(VenusDigitalContext context)
        {
            _context = context;
        }

        public IEnumerable<Categories> GetCategories()
        {
            return _context.Categories;
        }

        public Categories GetCategoryByCategoryId(int categoryId)
        {
            return _context.Categories
                .First(c => c.CategoryId == categoryId);
        }

        public IEnumerable<int> GetSelectedCategories(int productId)
        {
            return _context.SelectedCategory
                .Where(s => s.ProductId == productId)
                .Select(s => s.CategoryId)
                .ToList();
        }
    }
}
