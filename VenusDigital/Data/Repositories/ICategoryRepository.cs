using System.Collections.Generic;
using System.Linq;
using VenusDigital.Models;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Data.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Categories> GetCategories();
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
    }
}
