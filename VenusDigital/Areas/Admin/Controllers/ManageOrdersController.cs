using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenusDigital.Areas.Admin.Models;
using VenusDigital.Data;

namespace VenusDigital.Areas.Admin.Controllers
{
    public class ManageOrdersController : Controller
    {
        private VenusDigitalContext _context;

        public ManageOrdersController(VenusDigitalContext context)
        {
            _context = context;
        }

        public IActionResult GetUnprocessedOrders()
        {
            var finishedOrders= _context.Order
                .Include(o => o.OrderProcess)
                .Where(o=>o.IsFinally==true && !o.OrderProcess.IsProcessed)
                .Include(o=>o.OrderDetails)
                .ThenInclude(o=>o.Product)
                .Include(o=>o.Users)
                .ThenInclude(o=>o.PostalInformations)
                .Include(o=>o.OrderDetails.First().Product.Features)
                .Select(o=> new ShowUnprocessedOrders()
                {
                    ProductMainPrice = o.OrderDetails.First().Product.ProductMainPrice,
                    ProductTitle = o.OrderDetails.First().Product.ProductTitle,
                    ProductOnSalePrice = o.OrderDetails.First().Product.ProductOnSalePrice,
                    ProductId = o.OrderDetails.First().Product.ProductId,
                    Count = o.OrderDetails.First().Count,
                    TotalOrderPrice = o.TotalOrderPrice,
                    OrderId = o.OrderId,
                    TelephoneNumber = o.Users.PostalInformations.First().TelephoneNumber,
                    Address = o.Users.PostalInformations.First().Address,
                    ZipCode = o.Users.PostalInformations.First().ZipCode,
                    PhoneNumber = o.Users.PhoneNumber,
                    AppliedCoupon = o.AppliedCoupon,
                    Color = o.OrderDetails.First().Product.Features.Where(f=>f.FeatureTitle=="Color").Select(f=>f.FeatureValue).ToString(),
                    DetailId = o.OrderDetails.First().DetailId,
                    IsDelivered = o.OrderProcess.IsDelivered,
                    IsProcessed = o.OrderProcess.IsProcessed,
                    IsReferred = o.OrderProcess.IsReferred,
                    PaymentDate = o.PaymentDate,
                    PaymentTraceCode = o.PaymentTraceCode,
                    PosPostalInformationId = o.Users.PostalInformationId,
                    ReferredDescription = o.OrderProcess.ReferredDescription,
                    TotalPriceWithCoupon = o.TotalPriceWithCoupon,
                    UserId = o.UserId
                })
                .ToList();

            return View(finishedOrders);
        }
    }
}
