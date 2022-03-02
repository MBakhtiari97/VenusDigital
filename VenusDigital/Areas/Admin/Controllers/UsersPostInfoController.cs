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
    public class UsersPostInfoController : Controller
    {
        private readonly VenusDigitalContext _context;

        public UsersPostInfoController(VenusDigitalContext context)
        {
            _context = context;
        }

        #region UserInfoIndex

        // GET: Admin/UsersPostInfo
        public async Task<IActionResult> Index()
        {
            var venusDigitalContext = _context.PostalInformations.Include(p => p.User);
            return View(await venusDigitalContext.ToListAsync());
        }


        #endregion

        #region UserInfoDetails

        // GET: Admin/UsersPostInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postalInformations = await _context.PostalInformations
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostalInformationId == id);
            if (postalInformations == null)
            {
                return NotFound();
            }

            return View(postalInformations);
        }


        #endregion

        #region CreateUserInfo

        // GET: Admin/UsersPostInfo/Create
        public IActionResult Create(int userId)
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress");
            return View();
        }

        // POST: Admin/UsersPostInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostalInformationId,UserId,Address,ZipCode,TelephoneNumber")] PostalInformations postalInformations, int userId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postalInformations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress", postalInformations.UserId);
            return View(postalInformations);
        }

        #endregion

        #region UpdateUserInfo


        // GET: Admin/UsersPostInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postalInformations = await _context.PostalInformations.FindAsync(id);
            if (postalInformations == null)
            {
                return NotFound();
            }
            ViewBag.UserId = postalInformations.UserId;
            return View(postalInformations);
        }

        // POST: Admin/UsersPostInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostalInformationId,UserId,Address,ZipCode,TelephoneNumber")] PostalInformations postalInformations)
        {
            if (id != postalInformations.PostalInformationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postalInformations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostalInformationsExists(postalInformations.PostalInformationId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "EmailAddress", postalInformations.UserId);
            return View(postalInformations);
        }

        #endregion

        #region RemoveUserInfo


        // GET: Admin/UsersPostInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postalInformations = await _context.PostalInformations
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostalInformationId == id);
            if (postalInformations == null)
            {
                return NotFound();
            }

            return View(postalInformations);
        }

        // POST: Admin/UsersPostInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postalInformations = await _context.PostalInformations.FindAsync(id);
            _context.PostalInformations.Remove(postalInformations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion


        private bool PostalInformationsExists(int id)
        {
            return _context.PostalInformations.Any(e => e.PostalInformationId == id);
        }
    }
}
