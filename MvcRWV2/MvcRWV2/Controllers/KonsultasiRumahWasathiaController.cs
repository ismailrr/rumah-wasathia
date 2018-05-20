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
    public class KonsultasiRumahWasathiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KonsultasiRumahWasathiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KonsultasiRumahWasathia
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

            var konsultasiRumahWasathia = from s in _context.DaftarKonsultasiRumahWasathia
                       .Include(ee => ee.Kategori)
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                konsultasiRumahWasathia = konsultasiRumahWasathia.Where(s => s.Judul.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    konsultasiRumahWasathia = konsultasiRumahWasathia.OrderByDescending(s => s.Judul);
                    break;
                case "Name":
                    konsultasiRumahWasathia = konsultasiRumahWasathia.OrderBy(s => s.Judul);
                    break;
                case "Date":
                    konsultasiRumahWasathia = konsultasiRumahWasathia.OrderBy(s => s.Tanggal);
                    break;
                default:
                    konsultasiRumahWasathia = konsultasiRumahWasathia.OrderByDescending(s => s.Tanggal);
                    break;
            }
            int pageSize = 12;
            return View(await PaginatedList<KonsultasiRumahWasathia>.CreateAsync(konsultasiRumahWasathia.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: KonsultasiRumahWasathia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konsultasiRumahWasathia = await _context.DaftarKonsultasiRumahWasathia
                .Include(ee => ee.Kategori)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (konsultasiRumahWasathia == null)
            {
                return NotFound();
            }

            return View(konsultasiRumahWasathia);
        }

        // GET: KonsultasiRumahWasathia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KonsultasiRumahWasathia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Judul,Tanggal,Pertanyaan,Jawaban,Penulis")] KonsultasiRumahWasathia konsultasiRumahWasathia)
        {
            if (ModelState.IsValid)
            {
                konsultasiRumahWasathia.Tanggal = DateTime.Now;
                _context.Add(konsultasiRumahWasathia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(konsultasiRumahWasathia);
        }

        // GET: KonsultasiRumahWasathia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konsultasiRumahWasathia = await _context.DaftarKonsultasiRumahWasathia.SingleOrDefaultAsync(m => m.Id == id);
            if (konsultasiRumahWasathia == null)
            {
                return NotFound();
            }
            return View(konsultasiRumahWasathia);
        }

        // POST: KonsultasiRumahWasathia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Tanggal,Pertanyaan,Jawaban,Penulis")] KonsultasiRumahWasathia konsultasiRumahWasathia)
        {
            if (id != konsultasiRumahWasathia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(konsultasiRumahWasathia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KonsultasiRumahWasathiaExists(konsultasiRumahWasathia.Id))
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
            return View(konsultasiRumahWasathia);
        }

        // GET: KonsultasiRumahWasathia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konsultasiRumahWasathia = await _context.DaftarKonsultasiRumahWasathia
                .SingleOrDefaultAsync(m => m.Id == id);
            if (konsultasiRumahWasathia == null)
            {
                return NotFound();
            }

            return View(konsultasiRumahWasathia);
        }

        // POST: KonsultasiRumahWasathia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var konsultasiRumahWasathia = await _context.DaftarKonsultasiRumahWasathia.SingleOrDefaultAsync(m => m.Id == id);
            _context.DaftarKonsultasiRumahWasathia.Remove(konsultasiRumahWasathia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KonsultasiRumahWasathiaExists(int id)
        {
            return _context.DaftarKonsultasiRumahWasathia.Any(e => e.Id == id);
        }
    }
}
