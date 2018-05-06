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
    public class KajianAudioController : Controller
    {
        private readonly RWContext _context;

        public KajianAudioController(RWContext context)
        {
            _context = context;
        }

        // GET: KajianAudio
        public async Task<IActionResult> Index(
            string sortOrder,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            var kajianAudio = from s in _context.DaftarKajianAudio
                              .Include(ee => ee.Path)
                              .Include(ee => ee.Kategori)
                              select s;

            switch (sortOrder)
            {
                case "Date":
                    kajianAudio = kajianAudio.OrderBy(s => s.Tanggal);
                    break;
                default:
                    kajianAudio = kajianAudio.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 15;
            return View(await PaginatedList<KajianAudio>.CreateAsync(kajianAudio.AsNoTracking(), page ?? 1, pageSize));
        }

            // GET: KajianAudio/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kajianAudio = await _context.DaftarKajianAudio
                .Include(ee => ee.Path)
                .Include(ee => ee.Kategori)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kajianAudio == null)
            {
                return NotFound();
            }

            return View(kajianAudio);
        }

        // GET: KajianAudio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KajianAudio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Link,Tanggal")] KajianAudio kajianAudio)
        {
            if (ModelState.IsValid)
            {
                kajianAudio.Tanggal = DateTime.Now;
                kajianAudio.Link = kajianAudio.Link.Replace("<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"","");
                kajianAudio.Link = kajianAudio.Link.Replace("\"></iframe>", "");
                _context.Add(kajianAudio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kajianAudio);
        }

        // GET: KajianAudio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kajianAudio = await _context.DaftarKajianAudio.SingleOrDefaultAsync(m => m.Id == id);
            if (kajianAudio == null)
            {
                return NotFound();
            }
            return View(kajianAudio);
        }

        // POST: KajianAudio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Link,Tanggal")] KajianAudio kajianAudio)
        {
            if (id != kajianAudio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kajianAudio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KajianAudioExists(kajianAudio.Id))
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
            return View(kajianAudio);
        }

        // GET: KajianAudio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kajianAudio = await _context.DaftarKajianAudio
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kajianAudio == null)
            {
                return NotFound();
            }

            return View(kajianAudio);
        }

        // POST: KajianAudio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kajianAudio = await _context.DaftarKajianAudio.SingleOrDefaultAsync(m => m.Id == id);
            _context.DaftarKajianAudio.Remove(kajianAudio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KajianAudioExists(int id)
        {
            return _context.DaftarKajianAudio.Any(e => e.Id == id);
        }
    }
}
