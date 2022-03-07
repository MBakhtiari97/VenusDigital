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
    public class FeaturesController : Controller
    {
        private readonly VenusDigitalContext _context;

        public FeaturesController(VenusDigitalContext context)
        {
            _context = context;
        }

        #region FeatureIndex

        // GET: Admin/Features
        public async Task<IActionResult> Index(int pageId)
        {
            var features = _context.Features.Include(f => f.Products);
            var allFeatures = await features.ToListAsync();

            int take = 12;
            int skip = (pageId-1) * take;

            ViewBag.PageCount = (int)Math.Ceiling(allFeatures.Count() / (double)take);
            return View(allFeatures.Skip(skip).Take(take).ToList());
        }

        #endregion

        #region FeatureDetails

        // GET: Admin/Features/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var features = await _context.Features
                .Include(f => f.Products)
                .FirstOrDefaultAsync(m => m.FeatureId == id);
            if (features == null)
            {
                return NotFound();
            }

            return View(features);
        }


        #endregion

        #region NewFeature

        // GET: Admin/Features/Create
        public IActionResult Create(int productId)
        {
            return View();
        }

        // POST: Admin/Features/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeatureId,ProductId,FeatureTitle,FeatureValue")] Features features, int productId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(features);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductInStock", features.ProductId);
            return View(features);
        }

        #endregion

        #region UpdateFeature


        // GET: Admin/Features/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var features = await _context.Features.FindAsync(id);
            if (features == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductInStock", features.ProductId);
            return View(features);
        }

        // POST: Admin/Features/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeatureId,ProductId,FeatureTitle,FeatureValue")] Features features)
        {
            if (id != features.FeatureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(features);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeaturesExists(features.FeatureId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductInStock", features.ProductId);
            return View(features);
        }


        #endregion

        #region RemoveFeature


        // GET: Admin/Features/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var features = await _context.Features
                .Include(f => f.Products)
                .FirstOrDefaultAsync(m => m.FeatureId == id);
            if (features == null)
            {
                return NotFound();
            }

            return View(features);
        }

        // POST: Admin/Features/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var features = await _context.Features.FindAsync(id);
            _context.Features.Remove(features);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        #endregion


        private bool FeaturesExists(int id)
        {
            return _context.Features.Any(e => e.FeatureId == id);
        }
    }
}
