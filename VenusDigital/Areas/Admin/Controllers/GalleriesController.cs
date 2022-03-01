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
    public class GalleriesController : Controller
    {
        private readonly VenusDigitalContext _context;

        public GalleriesController(VenusDigitalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GalleriesViewModel Gallery { get; set; }

        #region GalleryIndex

        // GET: Admin/Galleries
        public async Task<IActionResult> Index()
        {
            var venusDigitalContext = _context.ProductGalleries.Include(p => p.Products);
            return View(await venusDigitalContext.ToListAsync());
        }

        #endregion

        #region GalleryDetails

        // GET: Admin/Galleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGalleries = await _context.ProductGalleries
                .Include(p => p.Products)
                .FirstOrDefaultAsync(m => m.GalleryId == id);
            if (productGalleries == null)
            {
                return NotFound();
            }

            return View(productGalleries);
        }

        #endregion

        #region CreateGallery

        // GET: Admin/Galleries/Create
        public IActionResult Create(int productId)
        {
            return View();
        }

        // POST: Admin/Galleries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalleryId,ProductId,ImageName,ImageRefersTo,ImageAltName")] ProductGalleries productGalleries, int productId)
        {

            if (ModelState.IsValid)
            {
                //Creating a unique name for image name and set it for file name 
                var imageGalleryName = Guid.NewGuid().ToString();
                if (Gallery.ImageName?.Length > 0)
                    productGalleries.ImageName = imageGalleryName
                                                 + Path.GetExtension(Gallery.ImageName.FileName);
                else
                {
                    productGalleries.ImageName = "Default.jpg";
                }
                _context.Add(productGalleries);
                await _context.SaveChangesAsync();

                if (Gallery.ImageName?.Length > 0)
                {
                    string filePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Images",
                        "pics",
                        imageGalleryName
                        + Path.GetExtension(Gallery.ImageName.FileName)
                    );
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Gallery.ImageName.CopyTo(stream);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductInStock", productGalleries.ProductId);
            return View(productGalleries);
        }


        #endregion

        #region UpdateGallery

        // GET: Admin/Galleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGalleries = await _context.ProductGalleries.FindAsync(id);
            if (productGalleries == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductInStock", productGalleries.ProductId);
            return View(productGalleries);
        }

        // POST: Admin/Galleries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GalleryId,ProductId,ImageName,ImageRefersTo,ImageAltName")] ProductGalleries productGalleries)
        {
            if (id != productGalleries.GalleryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentImgName = _context.ProductGalleries
                            .Find(productGalleries.GalleryId)
                            .ImageName;
                    string filePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Images",
                        "pics",
                        currentImgName
                    );
                    
                    //TODO:YOU SHOULD DELETE CURRENT IMAGE AND THEN ADD NEW IMAGE



                    _context.Update(productGalleries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductGalleriesExists(productGalleries.GalleryId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductInStock", productGalleries.ProductId);
            return View(productGalleries);
        }


        #endregion

        #region RemoveGallery

        // GET: Admin/Galleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGalleries = await _context.ProductGalleries
                .Include(p => p.Products)
                .FirstOrDefaultAsync(m => m.GalleryId == id);
            if (productGalleries == null)
            {
                return NotFound();
            }

            return View(productGalleries);
        }

        // POST: Admin/Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productGalleries = await _context.ProductGalleries.FindAsync(id);
            _context.ProductGalleries.Remove(productGalleries);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        #endregion

        private bool ProductGalleriesExists(int id)
        {
            return _context.ProductGalleries.Any(e => e.GalleryId == id);
        }
    }
}
