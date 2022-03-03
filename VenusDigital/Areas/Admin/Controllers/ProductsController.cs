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
    public class ProductsController : Controller
    {
        private readonly VenusDigitalContext _context;

        public ProductsController(VenusDigitalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<int> selectedGroups { get; set; }

        #region Product'sIndex

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        #endregion

        #region ProductDetail's


        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }


        #endregion

        #region NewProduct


        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductTitle,ProductShortDescription,ProductLongDescription,ProductInStock,ProductQuantityInStock,ProductMainPrice,GalleryId,ProductScore,TagId,ReviewId,ProductOnSalePrice,SalePercent,FeatureId,CreateDate")] Products products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                ProductGalleries newGallery = new ProductGalleries()
                {
                    ImageName = "Default.jpg",
                    ProductId = products.ProductId,
                    ImageAltName = products.ProductTitle
                };
                _context.ProductGalleries.Add(newGallery);
                await _context.SaveChangesAsync();

                if (selectedGroups.Any() && selectedGroups.Count > 0)
                {
                    foreach (var CategoryId in selectedGroups)
                    {
                        _context.SelectedCategory.Add(new SelectedCategory()
                        {
                            ProductId = products.ProductId,
                            CategoryId = CategoryId
                        });
                    }
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }


        #endregion

        #region UpdateProduct


        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _context
                .Categories
                .ToList();

            ViewBag.SelectedCategories = _context.SelectedCategory
                .Where(c => c.ProductId == id)
                .Select(c => c.CategoryId)
                .ToList();

            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductTitle,ProductShortDescription,ProductLongDescription,ProductInStock,ProductQuantityInStock,ProductMainPrice,GalleryId,ProductScore,TagId,ReviewId,ProductOnSalePrice,SalePercent,FeatureId,CreateDate")] Products products)
        {
            if (id != products.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (selectedGroups.Any() && selectedGroups.Count > 0)
                {
                    foreach (var CategoryId in selectedGroups)
                    {
                        _context.SelectedCategory.Add(new SelectedCategory()
                        {
                            ProductId = products.ProductId,
                            CategoryId = CategoryId
                        });
                    }
                }
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        #endregion

        #region RemoveProduct


        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        #endregion



        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
