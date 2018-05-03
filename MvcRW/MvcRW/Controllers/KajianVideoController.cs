using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcRW.Data;
using MvcRW.Models;

namespace MvcRW.Controllers
{
    public class KajianVideoController : Controller
    {
        private readonly RWContext _context;

        public KajianVideoController(RWContext context)
        {
            _context = context;
        }

        // GET: KajianVideo
        public async Task<IActionResult> Index(
            string sortOrder,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            var kajianVideo = from s in _context.DaftarKajianVideo
                              .Include(ee => ee.Path)
                              select s;

            switch (sortOrder)
            {
                case "date_desc":
                    kajianVideo = kajianVideo.OrderByDescending(s => s.Tanggal);
                    break;
                default:
                    kajianVideo = kajianVideo.OrderBy(s => s.Tanggal);
                    break;
            }

            int pageSize = 15;
            return View(await PaginatedList<KajianVideo>.CreateAsync(kajianVideo.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: KajianVideo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kajianVideo = await _context.DaftarKajianVideo
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kajianVideo == null)
            {
                return NotFound();
            }

            return View(kajianVideo);
        }

        // GET: KajianVideo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KajianVideo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Link,Tanggal")] KajianVideo kajianVideo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kajianVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kajianVideo);
        }

        // GET: KajianVideo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kajianVideo = await _context.DaftarKajianVideo.SingleOrDefaultAsync(m => m.Id == id);
            if (kajianVideo == null)
            {
                return NotFound();
            }
            return View(kajianVideo);
        }

        // POST: KajianVideo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Link,Tanggal")] KajianVideo kajianVideo)
        {
            if (id != kajianVideo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kajianVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KajianVideoExists(kajianVideo.Id))
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
            return View(kajianVideo);
        }

        // GET: KajianVideo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kajianVideo = await _context.DaftarKajianVideo
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kajianVideo == null)
            {
                return NotFound();
            }

            return View(kajianVideo);
        }

        // POST: KajianVideo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kajianVideo = await _context.DaftarKajianVideo.SingleOrDefaultAsync(m => m.Id == id);
            _context.DaftarKajianVideo.Remove(kajianVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KajianVideoExists(int id)
        {
            return _context.DaftarKajianVideo.Any(e => e.Id == id);
        }
    }
}
