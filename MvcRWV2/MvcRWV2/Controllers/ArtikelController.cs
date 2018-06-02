using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcRWV2.Data;
using MvcRWV2.Models;

namespace MvcRWV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ArtikelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int published = 1;
        private int trash = 2;

        public ArtikelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Artikel
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var artikel = from s in _context.DaftarArtikel
                          .Include(ee => ee.Path)
                          .Include(ee => ee.Kategori)
                          where s.Status == published   
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                artikel = artikel.Where(s => s.Judul.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    artikel = artikel.OrderByDescending(s => s.Judul);
                    break;
                case "Name":
                    artikel = artikel.OrderBy(s => s.Judul);
                    break;
                case "Date":
                    artikel = artikel.OrderBy(s => s.Tanggal);
                    break;
                default:
                    artikel = artikel.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 12;
            return View(await PaginatedList<Artikel>.CreateAsync(artikel.AsNoTracking(), page ?? 1, pageSize));
        }

        public async Task<IActionResult> List(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page,
            int? status)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "" : "Date";
            ViewData["Status"] = status;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var countItem = (from s in _context.DaftarArtikel
                             where s.Status == published 
                             select s).Count();
            var countTrash = (from s in _context.DaftarArtikel
                         where s.Status == trash
                         select s).Count();

            var artikel = from s in _context.DaftarArtikel
                          .Include(ee => ee.Path)
                          .Include(ee => ee.Kategori)
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                artikel = artikel.Where(s => s.Judul.Contains(searchString));
            }

            if (status == null)
            {
                artikel = artikel.Where(s => s.Status == published);
            }
            else if (status == trash)
            {
                artikel = artikel.Where(s => s.Status == trash);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    artikel = artikel.OrderByDescending(s => s.Judul);
                    break;
                case "Name":
                    artikel = artikel.OrderBy(s => s.Judul);
                    break;
                case "Date":
                    artikel = artikel.OrderBy(s => s.Tanggal);
                    break;
                default:
                    artikel = artikel.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 20;
            return View(await PaginatedList<Artikel>.CreateAsync(artikel.AsNoTracking(), page ?? 1, pageSize, countItem, countTrash));
        }

        // GET: Artikel/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikel = await _context.DaftarArtikel
                .Include(ee => ee.Path)
                .Include(ee => ee.Kategori)
                .AsNoTracking()
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
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Judul,Tanggal")] Artikel artikel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    artikel.Tanggal = DateTime.Now;
                    _context.Add(artikel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
                return View(artikel);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
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
        [HttpPost, ActionName("Edit")]
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
                    artikel.Tanggal = DateTime.Now;
                    _context.Update(artikel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
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
            return RedirectToAction(nameof(List));
        }

        private bool ArtikelExists(int id)
        {
            return _context.DaftarArtikel.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Trash(int id)
        {
            var artikel = await _context.DaftarArtikel.SingleOrDefaultAsync(m => m.Id == id);
            artikel.Status = trash;
            _context.DaftarArtikel.Update(artikel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Restore(int id)
        {
            var artikel = await _context.DaftarArtikel.SingleOrDefaultAsync(m => m.Id == id);
            artikel.Status = published;
            _context.DaftarArtikel.Update(artikel);
            await _context.SaveChangesAsync();
            return RedirectToAction("List", new{ status = trash });
        }
    }
}
