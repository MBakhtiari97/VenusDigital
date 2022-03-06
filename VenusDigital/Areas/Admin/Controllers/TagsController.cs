using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VenusDigital.Areas.Admin.Models;
using VenusDigital.Data;
using VenusDigital.Models;

namespace VenusDigital.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagsController : Controller
    {
        private readonly VenusDigitalContext _context;

        public TagsController(VenusDigitalContext context)
        {
            _context = context;
        }
        [BindProperty]
        public TagsViewModel Tags { get; set; }

        #region TagIndex

        // GET: Admin/Tags
        public async Task<IActionResult> Index(int pageId)
        {
            var tags = _context.Tags
                .Include(t => t.Products);
            var allTags = await tags.ToListAsync();

            //For Pagination
            int take = 12;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(allTags.Count() / (double)take);
            return View(allTags.Skip(skip).Take(take).ToList());
        }


        #endregion

        #region TagDetail

        // GET: Admin/Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tags = await _context.Tags
                .Include(t => t.Products)
                .FirstOrDefaultAsync(m => m.TagId == id);
            if (tags == null)
            {
                return NotFound();
            }

            return View(tags);
        }

        #endregion

        #region CreateTag
        // GET: Admin/Tags/Create
        public IActionResult Create(int productId)
        {
            return View();
        }

        // POST: Admin/Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TagId,ProductId,Tag")] Tags tags, int productId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tags);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductInStock", tags.ProductId);
            return View(tags);
        }

        #endregion

        #region UpdateTag

        // GET: Admin/Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tags = await _context.Tags.FindAsync(id);
            if (tags == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductInStock", tags.ProductId);
            return View(tags);
        }

        // POST: Admin/Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TagId,ProductId,Tag")] Tags tags)
        {
            if (id != tags.TagId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tags);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagsExists(tags.TagId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductInStock", tags.ProductId);
            return View(tags);
        }


        #endregion

        #region RemoveTag

        // GET: Admin/Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tags = await _context.Tags
                .Include(t => t.Products)
                .FirstOrDefaultAsync(m => m.TagId == id);
            if (tags == null)
            {
                return NotFound();
            }

            return View(tags);
        }

        // POST: Admin/Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tags = await _context.Tags.FindAsync(id);
            _context.Tags.Remove(tags);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        #endregion

        private bool TagsExists(int id)
        {
            return _context.Tags.Any(e => e.TagId == id);
        }
    }
}
