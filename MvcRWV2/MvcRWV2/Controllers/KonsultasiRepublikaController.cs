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
using MvcRWV2.Models.KonsultasiRepublikaViewModels;

namespace MvcRWV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class KonsultasiRepublikaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int published = 1;
        private int trash = 2;

        public KonsultasiRepublikaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KonsultasiRepublika
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

            IndexKonsultasiRepublikaViewModel mymodel = new IndexKonsultasiRepublikaViewModel();

            var konsultasiRepublika = from s in _context.DaftarKonsultasiRepublika
                          .Include(ee => ee.Kategori)
                          where s.Status == published
                          select s;

            mymodel.BukuModel = from s in _context.DaftarBuku
                       .Include(ee => ee.Kategori)
                       .Include(ee => ee.Path)
                       .Take(4)
                       where s.Status == published
                       select s;
            mymodel.ArtikelModel = from s in _context.DaftarArtikel
                       .Include(ee => ee.Kategori)
                       .Take(4)
                       where s.Status == published
                       select s;

            var buku = mymodel.BukuModel;
            var atikel = mymodel.ArtikelModel;

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
            mymodel.KonsultasiRepublikaModel = await PaginatedList<KonsultasiRepublika>.CreateAsync(konsultasiRepublika.AsNoTracking(), page ?? 1, pageSize);
            return View(mymodel);
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

            var countItem = (from s in _context.DaftarKonsultasiRepublika
                             where s.Status == published
                             select s).Count();
            var countTrash = (from s in _context.DaftarKonsultasiRepublika
                              where s.Status == trash
                              select s).Count();

            var konsultasiRepublika = from s in _context.DaftarKonsultasiRepublika
                          .Include(ee => ee.Kategori)
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                konsultasiRepublika = konsultasiRepublika.Where(s => s.Judul.Contains(searchString));
            }

            if (status == null)
            {
                konsultasiRepublika = konsultasiRepublika.Where(s => s.Status == published);
            }
            else if (status == trash)
            {
                konsultasiRepublika = konsultasiRepublika.Where(s => s.Status == trash);
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

            int pageSize = 20;
            return View(await PaginatedList<KonsultasiRepublika>.CreateAsync(konsultasiRepublika.AsNoTracking(), page ?? 1, pageSize, countItem, countTrash));
        }

        // GET: KonsultasiRepublika/Details/5
        [AllowAnonymous]
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Link,Tanggal,Kategori,Tag,Penulis,Status")] KonsultasiRepublika konsultasiRepublika)
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

        public async Task<IActionResult> Trash(int id)
        {
            var konsultasiRepublika = await _context.DaftarKonsultasiRepublika.SingleOrDefaultAsync(m => m.Id == id);
            konsultasiRepublika.Status = trash;
            _context.DaftarKonsultasiRepublika.Update(konsultasiRepublika);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Restore(int id)
        {
            var konsultasiRepublika = await _context.DaftarKonsultasiRepublika.SingleOrDefaultAsync(m => m.Id == id);
            konsultasiRepublika.Status = published;
            _context.DaftarKonsultasiRepublika.Update(konsultasiRepublika);
            await _context.SaveChangesAsync();
            return RedirectToAction("List", new { status = trash });
        }
    }
}
