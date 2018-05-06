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
    public class BukuController : Controller
    {
        private readonly RWContext _context;

        public BukuController(RWContext context)
        {
            _context = context;
        }

        // GET: Buku
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

            var buku = from s in _context.DaftarBuku
                       .Include(ee => ee.Path)
                       .Include(ee => ee.Kategori)
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                buku = buku.Where(s => s.Judul.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    buku = buku.OrderByDescending(s => s.Judul);
                    break;
                case "Date":
                    buku = buku.OrderBy(s => s.Tanggal);
                    break;
                case "date_desc":
                    buku = buku.OrderByDescending(s => s.Tanggal);
                    break;
                default:
                    buku = buku.OrderBy(s => s.Judul);
                    break;
            }
            int pageSize = 12;
            return View(await PaginatedList<Buku>.CreateAsync(buku.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Buku/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.DaftarBuku
                .Include(ee => ee.Path)
                .Include(ee => ee.Kategori)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (buku == null)
            {
                return NotFound();
            }

            return View(buku);
        }

        // GET: Buku/Create
        public IActionResult Create()
        {
            var model = new Buku { Tanggal = DateTime.Now };
            return View(model);
        }

        // POST: Buku/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Judul,Penulis,Terbitan,ISBN,Deskripsi,Tebal,Tanggal")] Buku buku)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buku);
        }

        // GET: Buku/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.DaftarBuku.SingleOrDefaultAsync(m => m.Id == id);
            if (buku == null)
            {
                return NotFound();
            }
            return View(buku);
        }

        // POST: Buku/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Penulis,Terbitan,ISBN,Deskripsi,Tebal,Tanggal")] Buku buku)
        {
            if (id != buku.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BukuExists(buku.Id))
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
            return View(buku);
        }

        // GET: Buku/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buku = await _context.DaftarBuku
                .SingleOrDefaultAsync(m => m.Id == id);
            if (buku == null)
            {
                return NotFound();
            }

            return View(buku);
        }

        // POST: Buku/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buku = await _context.DaftarBuku.SingleOrDefaultAsync(m => m.Id == id);
            _context.DaftarBuku.Remove(buku);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BukuExists(int id)
        {
            return _context.DaftarBuku.Any(e => e.Id == id);
        }
    }
}
