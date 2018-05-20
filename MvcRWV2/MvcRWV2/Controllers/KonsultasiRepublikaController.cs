using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcRWV2.Data;
using MvcRWV2.Models;

namespace MvcRWV2.Controllers
{
    public class KonsultasiRepublikaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KonsultasiRepublikaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KonsultasiRepublika
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var konsultasiRepublika = from s in _context.DaftarKonsultasiRepublika
                          .Include(ee => ee.Kategori)
                                      select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                konsultasiRepublika = konsultasiRepublika.Where(s => s.Judul.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    konsultasiRepublika = konsultasiRepublika.OrderByDescending(s => s.Judul);
                    break;
                case "Name":
                    konsultasiRepublika = konsultasiRepublika.OrderBy(s => s.Judul);
                    break;
                case "Date":
                    konsultasiRepublika = konsultasiRepublika.OrderBy(s => s.Tanggal);
                    break;
                default:
                    konsultasiRepublika = konsultasiRepublika.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 12;
            return View(await PaginatedList<KonsultasiRepublika>.CreateAsync(konsultasiRepublika.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: KonsultasiRepublika/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konsultasiRepublika = await _context.DaftarKonsultasiRepublika
                .Include(ee => ee.Kategori)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (konsultasiRepublika == null)
            {
                return NotFound();
            }

            return View(konsultasiRepublika);
        }

        // GET: KonsultasiRepublika/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KonsultasiRepublika/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Judul,Link,Tanggal")] KonsultasiRepublika konsultasiRepublika)
        {
            if (ModelState.IsValid)
            {
                konsultasiRepublika.Tanggal = DateTime.Now;
                _context.Add(konsultasiRepublika);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToPage("Create");
        }

        // GET: KonsultasiRepublika/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konsultasiRepublika = await _context.DaftarKonsultasiRepublika.SingleOrDefaultAsync(m => m.Id == id);
            if (konsultasiRepublika == null)
            {
                return NotFound();
            }
            return View(konsultasiRepublika);
        }

        // POST: KonsultasiRepublika/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Link,Tanggal")] KonsultasiRepublika konsultasiRepublika)
        {
            if (id != konsultasiRepublika.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(konsultasiRepublika);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KonsultasiRepublikaExists(konsultasiRepublika.Id))
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
            return View(konsultasiRepublika);
        }

        // GET: KonsultasiRepublika/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konsultasiRepublika = await _context.DaftarKonsultasiRepublika
                .SingleOrDefaultAsync(m => m.Id == id);
            if (konsultasiRepublika == null)
            {
                return NotFound();
            }

            return View(konsultasiRepublika);
        }

        // POST: KonsultasiRepublika/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var konsultasiRepublika = await _context.DaftarKonsultasiRepublika.SingleOrDefaultAsync(m => m.Id == id);
            _context.DaftarKonsultasiRepublika.Remove(konsultasiRepublika);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KonsultasiRepublikaExists(int id)
        {
            return _context.DaftarKonsultasiRepublika.Any(e => e.Id == id);
        }
    }
}
