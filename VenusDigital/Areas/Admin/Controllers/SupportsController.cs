using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VenusDigital.Data;
using VenusDigital.Models;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupportsController : Controller
    {
        private readonly VenusDigitalContext _context;

        public SupportsController(VenusDigitalContext context)
        {
            _context = context;
        }



        #region SupportRequestIndex

        // GET: Admin/Supports
        public async Task<IActionResult> Index()
        {
            return View(await _context.Supports.ToListAsync());
        }

        #endregion

        #region SupportRequestDetails

        // GET: Admin/Supports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supports = await _context.Supports
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (supports == null)
            {
                return NotFound();
            }

            return View(supports);
        }


        #endregion

        #region UpdateSupportRequest

        // GET: Admin/Supports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supports = await _context.Supports.FindAsync(id);
            if (supports == null)
            {
                return NotFound();
            }
            return View(supports);
        }

        // POST: Admin/Supports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,UserFullName,UserEmailAddress,UserIpAddress,RequestTitle,RequestDescription,RequestCode,IsAnswered,AnswerDescription,AnswerDate")] Supports supports)
        {
            if (id != supports.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supports);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupportsExists(supports.ContactId))
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
            return View(supports);
        }


        #endregion

        #region RemoveSupportRequest

        // GET: Admin/Supports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supports = await _context.Supports
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (supports == null)
            {
                return NotFound();
            }

            return View(supports);
        }

        // POST: Admin/Supports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supports = await _context.Supports.FindAsync(id);
            _context.Supports.Remove(supports);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion


        private bool SupportsExists(int id)
        {
            return _context.Supports.Any(e => e.ContactId == id);
        }
    }
}
