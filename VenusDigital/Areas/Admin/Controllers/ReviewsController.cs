using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VenusDigital.Data;
using VenusDigital.Models;

namespace VenusDigital.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewsController : Controller
    {
        private readonly VenusDigitalContext _context;

        public ReviewsController(VenusDigitalContext context)
        {
            _context = context;
        }

        #region ReviewsIndex

        // GET: Admin/Reviews
        public async Task<IActionResult> Index(int pageId)
        {
            var reviews = _context.Reviews
                .Include(r => r.Products)
                .Include(r => r.Users);
            var allReviews = await reviews.ToListAsync();

            //For Pagination
            int take = 12;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(allReviews.Count() / (double)take);

            return View(allReviews.Skip(skip).Take(take).ToList());
        }

        #endregion

        #region ReviewsDetails

        // GET: Admin/Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .Include(r => r.Products)
                .Include(r => r.Users)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }


        #endregion

        #region CreateReview


        // GET: Admin/Reviews/Create
        public IActionResult Create(int productId)
        {
            return View();
        }

        // POST: Admin/Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,ProductId,ReviewTitle,UserId,ReviewDescription,ReviewScore,ReviewCreateDate,IsPublished")] Reviews reviews,int productId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reviews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductInStock", reviews.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress", reviews.UserId);
            return View(reviews);
        }

        #endregion

        #region UpdateReview



        // GET: Admin/Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews.FindAsync(id);
            if (reviews == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductInStock", reviews.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress", reviews.UserId);
            ViewBag.UserId = reviews.UserId;
            return View(reviews);
        }

        // POST: Admin/Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewId,ProductId,ReviewTitle,UserId,ReviewDescription,ReviewScore,ReviewCreateDate,IsPublished")] Reviews reviews)
        {
            if (id != reviews.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewsExists(reviews.ReviewId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductInStock", reviews.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress", reviews.UserId);
            return View(reviews);
        }

        #endregion

        #region RemoveReview



        // GET: Admin/Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .Include(r => r.Products)
                .Include(r => r.Users)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // POST: Admin/Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviews = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        #endregion


        private bool ReviewsExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewId == id);
        }
    }
}
