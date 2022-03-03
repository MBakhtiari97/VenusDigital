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
    public class SlidersController : Controller
    {
        private readonly VenusDigitalContext _context;

        public SlidersController(VenusDigitalContext context)
        {
            _context = context;
        }

        [BindProperty] 
        public SliderViewModel Slider { get; set; }


        #region SliderIndex

        // GET: Admin/Sliders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Slider.ToListAsync());
        }


        #endregion

        #region SliderDetail's

        // GET: Admin/Sliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
                .FirstOrDefaultAsync(m => m.SlideId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }


        #endregion

        #region NewSlider

        // GET: Admin/Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SlideId,SlideName,SlideAltName,SlideRefersTo")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                //Creating a unique name for image name and set it for file name 
                var slideName = Guid.NewGuid().ToString();
                if (Slider.SlideName?.Length > 0)
                    slider.SlideName = slideName
                                       + Path.GetExtension(Slider.SlideName.FileName);
                else
                {
                    slider.SlideName = "Default.png";
                }

                _context.Add(slider);
                await _context.SaveChangesAsync();

                //Save image in server
                if (Slider.SlideName?.Length > 0)
                {
                    string filePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Images",
                        "slider",
                        slideName
                        + Path.GetExtension(Slider.SlideName.FileName)
                    );
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Slider.SlideName.CopyTo(stream);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }


        #endregion

        #region UpdateSlider

        // GET: Admin/Sliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: Admin/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SlideId,SlideName,SlideAltName,SlideRefersTo")] Slider slider)
        {
            if (id != slider.SlideId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Gathering current name
                    var currentName = _context.Slider.AsNoTracking()
                        .FirstOrDefault(pg => pg.SlideId == id);

                    //Checking if user want to change the image
                    if (Slider.SlideName != null)
                    {
                        string oldFilePath = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "Images",
                            "slider",
                            currentName.SlideName
                        );
                        //Deleting existed image
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }

                        //Giving new name to photo and saving it on server
                        var newSlideName = Guid.NewGuid().ToString();
                        string newFilePath = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "Images",
                            "slider",
                            newSlideName + Path.GetExtension(Slider.SlideName.FileName)
                        );


                        slider.SlideName = newSlideName
                                                     + Path.GetExtension(Slider.SlideName.FileName);
                        await using var stream = new FileStream(newFilePath, FileMode.Create);
                        await Slider.SlideName.CopyToAsync(stream);
                    }
                    else
                    {
                        slider.SlideName = currentName.SlideName;
                    }
                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.SlideId))
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
            return View(slider);
        }


        #endregion

        #region RemoveSlider

        // GET: Admin/Sliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
                .FirstOrDefaultAsync(m => m.SlideId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Admin/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slider = await _context.Slider.FindAsync(id);
            _context.Slider.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        private bool SliderExists(int id)
        {
            return _context.Slider.Any(e => e.SlideId == id);
        }
    }
}
