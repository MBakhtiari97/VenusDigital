using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VenusDigital.Areas.Admin.Models;
using VenusDigital.Data;
using VenusDigital.Models;

namespace VenusDigital.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly VenusDigitalContext _context;

        public OrdersController(VenusDigitalContext context)
        {
            _context = context;
        }

        #region OrderIndex

        // GET: Admin/Orders
        public async Task<IActionResult> Index()
        {
            var venusDigitalContext = _context.Order
                .Include(o => o.Users)
                .Where(o => o.IsFinally);

            return View(await venusDigitalContext.ToListAsync());
        }

        #endregion

        #region ProcessOrder

        public async Task<IActionResult> ProcessOrder()
        {
            var venusDigitalContext = _context.Order
                .Include(o => o.Users)
                .Where(o => o.IsFinally && !o.IsProcessed);

            return View(await venusDigitalContext.ToListAsync());
        }

        #endregion

        #region PrintOrder

        public IActionResult OrderPrint(int orderId)
        {
            var details = _context.Order
                .Where(o => o.OrderId == orderId)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .Include(o => o.Users)
                .ThenInclude(o => o.PostalInformations)
                .Select(s => new PurchaseInvoiceViewModel()
                {
                    ProductTitle = s.OrderDetails.First().Product.ProductTitle,
                    Count = s.OrderDetails.First().Count,
                    OrderId = s.OrderId,
                    Address = s.Users.PostalInformations.First().Address,
                    ZipCode = s.Users.PostalInformations.First().ZipCode,
                    UserName = s.Users.UserName,
                    PhoneNumber = s.Users.PhoneNumber,
                    Email = s.Users.EmailAddress,
                    OrderTotalPrice = s.TotalOrderPrice,
                    ProductPrice = s.OrderDetails.First().Product.ProductMainPrice
                }).ToList();

            return View(details);
        }


        #endregion

        #region OrderDetail's

        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        #endregion

        #region UpdateOrderStatus

        // GET: Admin/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress", order.UserId);
            ViewBag.UserId = order.UserId;
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,UserId,CreateDate,IsFinally,TotalOrderPrice,TotalPriceWithCoupon,AppliedCoupon,PaymentDate,PaymentTraceCode,IsProcessed,IsDelivered")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress", order.UserId);
            return View(order);
        }


        #endregion

        #region RemoveOrder

        // GET: Admin/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        #endregion

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
