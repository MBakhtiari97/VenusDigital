using System;
using System.Collections.Generic;
using System.IO;
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
    public class CategoriesController : Controller
    {
        private readonly VenusDigitalContext _context;

        public CategoriesController(VenusDigitalContext context)
        {
            _context = context;
        }
        [BindProperty] 
        public CategoriesViewModel Category { get; set; }

        #region CategoryIndex

        // GET: Admin/Categories
        public async Task<IActionResult> Index(int pageId)
        {
            var categories = _context.Categories
                .Include(c => c.Category);

            var allCategories = await categories.ToListAsync();
            //For Pagination
            int take = 12;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(allCategories.Count() / (double)take);

            return View(allCategories.Skip(skip).Take(take).ToList());
        }


        #endregion

        #region CategoryDetails
        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        #endregion
        
        #region CreateCategory

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,ParentId,ParentCategoryBanner,CategoryBanner")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                //Getting name from bind property
                if (Category.ParentCategoryBanner?.Length > 0)
                    categories.ParentCategoryBanner = categories.CategoryId
                                                      + Path.GetExtension(Category.ParentCategoryBanner.FileName);

                if (Category.CategoryBanner?.Length > 0)
                    categories.CategoryBanner = categories.CategoryId
                                                + Path.GetExtension(Category.CategoryBanner.FileName);

                //Saving insert values on database
                _context.Add(categories);
                await _context.SaveChangesAsync();

                //checking for images and inserting it on server
                if (Category.ParentCategoryBanner?.Length > 0)
                {
                    string filePathBanner = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Images",
                        "categoriesBanner",
                        categories.CategoryId + Path.GetExtension(Category.ParentCategoryBanner.FileName)
                    );
                    using (var stream = new FileStream(filePathBanner, FileMode.Create))
                    {
                        Category.ParentCategoryBanner.CopyTo(stream);
                    }
                }
                if (Category.CategoryBanner?.Length > 0)
                {
                    string filePathBigBanner = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Images",
                        "categoriesBigBanner",
                        categories.CategoryId + Path.GetExtension(Category.CategoryBanner.FileName)
                    );
                    using (var stream = new FileStream(filePathBigBanner, FileMode.Create))
                    {
                        Category.CategoryBanner.CopyTo(stream);
                    }
                }
                //redirecting after completing
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", categories.CategoryId);
            return View(categories);
        }

        #endregion

        #region CreateSubCategory

        // GET: Admin/Categories/Create
        public IActionResult CreateSubCategory()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewBag.ParentGroups = _context.Categories
                .Where(c => c.ParentId == null)
                .ToList();

            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubCategory([Bind("CategoryId,CategoryName,ParentId,ParentCategoryBanner,CategoryBanner")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", categories.CategoryId);
            return View(categories);
        }


        #endregion

        #region UpdateCategory

       
        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories.FindAsync(id);
            ViewBag.ParentGroups = _context.Categories
                .Where(c => c.ParentId == null)
                .ToList();
            if (categories == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", categories.CategoryId);
            return View(categories);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,ParentId,ParentCategoryBanner,CategoryBanner")] Categories categories)
        {
            if (id != categories.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categories);
                    await _context.SaveChangesAsync();

                    //checking for images and inserting it on server
                    if (Category.ParentCategoryBanner?.Length > 0)
                    {
                        string filePathBanner = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "Images",
                            "categoriesBanner",
                            categories.CategoryId + Path.GetExtension(Category.ParentCategoryBanner.FileName)
                        );
                        using (var stream = new FileStream(filePathBanner, FileMode.Create))
                        {
                            Category.ParentCategoryBanner.CopyTo(stream);
                        }
                    }
                    if (Category.CategoryBanner?.Length > 0)
                    {
                        string filePathBigBanner = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "Images",
                            "categoriesBigBanner",
                            categories.CategoryId + Path.GetExtension(Category.CategoryBanner.FileName)
                        );
                        using (var stream = new FileStream(filePathBigBanner, FileMode.Create))
                        {
                            Category.CategoryBanner.CopyTo(stream);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesExists(categories.CategoryId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", categories.CategoryId);
            return View(categories);
        }

        #endregion

        #region DeleteCategory

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.CategoryId == id);

            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            if (categories.ParentId == null)
            {
                _context.Categories
                    .Where(c => c.ParentId == id)
                    .ToList()
                    .ForEach(c => _context.Categories.Remove(c)
                    );
            }
            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        #endregion

        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
