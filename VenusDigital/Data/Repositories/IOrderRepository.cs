using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VenusDigital.Models;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Data.Repositories
{
    public interface IOrderRepository
    {
        Order GetOrderByUserId(int userId);
    }

    public class OrderRepository : IOrderRepository
    {
        private VenusDigitalContext _context;

        public OrderRepository(VenusDigitalContext context)
        {
            _context = context;
        }
        public Order GetOrderByUserId(int userId)
        {
            return _context.Order
                .Where(o => o.UserId == userId && !o.IsFinally)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .ThenInclude(p=>p.ProductGalleries)
                .FirstOrDefault();
        }
    }
}
