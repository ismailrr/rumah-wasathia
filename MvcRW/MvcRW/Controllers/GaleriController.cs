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
    public class GaleriController : Controller
    {
        private readonly RWContext _context;

        public GaleriController(RWContext context)
        {
            _context = context;
        }

        // GET: Galeri
        public async Task<IActionResult> Index(
            string sortOrder,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            var galeri = from s in _context.DaftarGaleri
                         .Include(ee => ee.Path)
                         .Include(ee => ee.Kategori)
                         select s;

            switch (sortOrder)
            {
                case "name_desc":
                    galeri = galeri.OrderByDescending(s => s.Judul);
                    break;
                case "Name":
                    galeri = galeri.OrderBy(s => s.Judul);
                    break;
                case "Date":
                    galeri = galeri.OrderBy(s => s.Tanggal);
                    break;
                default:
                    galeri = galeri.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 15;
            return View(await PaginatedList<Galeri>.CreateAsync(galeri.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Galeri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galeri = await _context.DaftarGaleri
                .Include(ee => ee.Path)
                .Include(ee => ee.Kategori)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (galeri == null)
            {
                return NotFound();
            }

            return View(galeri);
        }

        // GET: Galeri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Galeri/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Judul,Tanggal")] Galeri galeri)
        {
            if (ModelState.IsValid)
            {
                galeri.Tanggal = DateTime.Now;
                _context.Add(galeri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(galeri);
        }

        // GET: Galeri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galeri = await _context.DaftarGaleri.SingleOrDefaultAsync(m => m.Id == id);
            if (galeri == null)
            {
                return NotFound();
            }
            return View(galeri);
        }

        // POST: Galeri/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Tanggal")] Galeri galeri)
        {
            if (id != galeri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galeri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GaleriExists(galeri.Id))
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
            return View(galeri);
        }

        // GET: Galeri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galeri = await _context.DaftarGaleri
                .SingleOrDefaultAsync(m => m.Id == id);
            if (galeri == null)
            {
                return NotFound();
            }

            return View(galeri);
        }

        // POST: Galeri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galeri = await _context.DaftarGaleri.SingleOrDefaultAsync(m => m.Id == id);
            _context.DaftarGaleri.Remove(galeri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GaleriExists(int id)
        {
            return _context.DaftarGaleri.Any(e => e.Id == id);
        }
    }
}
