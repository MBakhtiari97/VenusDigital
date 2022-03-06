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
    public class NewslettersController : Controller
    {
        private readonly VenusDigitalContext _context;

        public NewslettersController(VenusDigitalContext context)
        {
            _context = context;
        }

        #region NewsletterIndex

        // GET: Admin/Newsletters
        public async Task<IActionResult> Index(int pageId)
        {
            var newsLetter = await _context.Newsletters.ToListAsync();
            //For Pagination
            int take = 12;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(newsLetter.Count() / (double)take);

            return View(newsLetter.Skip(skip).Take(take).ToList());

        }

        #endregion

        #region NewsletterDetails

        // GET: Admin/Newsletters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletters = await _context.Newsletters
                .FirstOrDefaultAsync(m => m.NewsletterId == id);
            if (newsletters == null)
            {
                return NotFound();
            }

            return View(newsletters);
        }


        #endregion

        #region SubscribeToNewsletter



        // GET: Admin/Newsletters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Newsletters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsletterId,NewslettersSubedUserEmail")] Newsletters newsletters)
        {
            if (ModelState.IsValid)
            {
                if (!_context.Newsletters.Any(u =>
                        u.NewslettersSubedUserEmail == newsletters.NewslettersSubedUserEmail.Trim().ToLower()))
                {
                    newsletters.NewslettersSubedUserEmail = newsletters.NewslettersSubedUserEmail.Trim().ToLower();
                    _context.Add(newsletters);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Toast Notification
                }
            }
            return View(newsletters);
        }

        #endregion

        #region UpdateUserInNewsletter


        // GET: Admin/Newsletters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletters = await _context.Newsletters.FindAsync(id);
            if (newsletters == null)
            {
                return NotFound();
            }
            return View(newsletters);
        }

        // POST: Admin/Newsletters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsletterId,NewslettersSubedUserEmail")] Newsletters newsletters)
        {
            if (id != newsletters.NewsletterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!_context.Newsletters.Any(u =>
                            u.NewslettersSubedUserEmail == newsletters.NewslettersSubedUserEmail.Trim().ToLower()))
                    {
                        newsletters.NewslettersSubedUserEmail = newsletters.NewslettersSubedUserEmail.Trim().ToLower();
                        _context.Update(newsletters);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        //TOAST NOTIFICATION
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewslettersExists(newsletters.NewsletterId))
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
            return View(newsletters);
        }


        #endregion

        #region RemoveUserFromNewsletter



        // GET: Admin/Newsletters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletters = await _context.Newsletters
                .FirstOrDefaultAsync(m => m.NewsletterId == id);
            if (newsletters == null)
            {
                return NotFound();
            }

            return View(newsletters);
        }

        // POST: Admin/Newsletters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsletters = await _context.Newsletters.FindAsync(id);
            _context.Newsletters.Remove(newsletters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion


        private bool NewslettersExists(int id)
        {
            return _context.Newsletters.Any(e => e.NewsletterId == id);
        }
    }
}
