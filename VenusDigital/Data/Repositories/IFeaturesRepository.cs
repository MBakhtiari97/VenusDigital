using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using VenusDigital.Models;

namespace VenusDigital.Data.Repositories
{
    public interface IFeaturesRepository
    {
        IEnumerable<Features> GetAllFeaturesByProductId(int ProductId);
    }

    public class FeatureRepository : IFeaturesRepository
    {
        private VenusDigitalContext _context;

        public FeatureRepository(VenusDigitalContext context)
        {
            _context = context;
        }
        public IEnumerable<Features> GetAllFeaturesByProductId(int ProductId)
        {
            return _context.Features
                .Where(f => f.ProductId == ProductId)
                .ToList();
        }
    }
}
