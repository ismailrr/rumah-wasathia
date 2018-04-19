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
    public class ArtikelController : Controller
    {
        private readonly RWContext _context;

        public ArtikelController(RWContext context)
        {
            _context = context;
        }

        // GET: Artikel
        public async Task<IActionResult> Index()
        {
            return View(await _context.DaftarArtikel.ToListAsync());
        }

        // GET: Artikel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikel = await _context.DaftarArtikel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (artikel == null)
            {
                return NotFound();
            }

            return View(artikel);
        }

        // GET: Artikel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artikel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Judul,Tanggal")] Artikel artikel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artikel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artikel);
        }

        // GET: Artikel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikel = await _context.DaftarArtikel.SingleOrDefaultAsync(m => m.Id == id);
            if (artikel == null)
            {
                return NotFound();
            }
            return View(artikel);
        }

        // POST: Artikel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Tanggal")] Artikel artikel)
        {
            if (id != artikel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artikel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtikelExists(artikel.Id))
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
            return View(artikel);
        }

        // GET: Artikel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikel = await _context.DaftarArtikel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (artikel == null)
            {
                return NotFound();
            }

            return View(artikel);
        }

        // POST: Artikel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artikel = await _context.DaftarArtikel.SingleOrDefaultAsync(m => m.Id == id);
            _context.DaftarArtikel.Remove(artikel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtikelExists(int id)
        {
            return _context.DaftarArtikel.Any(e => e.Id == id);
        }
    }
}
