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
using MvcRWV2.Models.BukuViewModels;

namespace MvcRWV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class BukuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int published = 1;
        private int trash = 2;

        public BukuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Buku
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

            IndexBukuViewModel mymodel = new IndexBukuViewModel();

            var buku = from s in _context.DaftarBuku
                       .Include(ee => ee.Path)
                       .Include(ee => ee.Kategori)
                       where s.Status == published
                       select s;

            mymodel.ArtikelModel = from s in _context.DaftarArtikel
                       .Include(ee => ee.Kategori)
                       .Include(ee => ee.Path)
                       .Take(4)
                       where s.Status == published
                       select s;
            mymodel.KonsultasiRepublikaModel = from s in _context.DaftarKonsultasiRepublika
                       .Include(ee => ee.Kategori)
                       .Take(4)
                       where s.Status == published
                       select s;

            var atikel = mymodel.ArtikelModel;
            var konsultasiRepublika = mymodel.KonsultasiRepublikaModel;

            if (!String.IsNullOrEmpty(searchString))
            {
                buku = buku.Where(s => s.Judul.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    buku = buku.OrderByDescending(s => s.Judul);
                    break;
                case "Name":
                    buku = buku.OrderBy(s => s.Judul);
                    break;
                case "Date":
                    buku = buku.OrderBy(s => s.Tanggal);
                    break;
                default:
                    buku = buku.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 12;
            mymodel.BukuModel = await PaginatedList<Buku>.CreateAsync(buku.AsNoTracking(), page ?? 1, pageSize);
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

            var countItem = (from s in _context.DaftarBuku
                             where s.Status == published
                             select s).Count();
            var countTrash = (from s in _context.DaftarBuku
                              where s.Status == trash
                              select s).Count();

            var buku = from s in _context.DaftarBuku
                       .Include(ee => ee.Path)
                       .Include(ee => ee.Kategori)
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                buku = buku.Where(s => s.Judul.Contains(searchString));
            }

            if (status == null)
            {
                buku = buku.Where(s => s.Status == published);
            }
            else if (status == trash)
            {
                buku = buku.Where(s => s.Status == trash);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    buku = buku.OrderByDescending(s => s.Judul);
                    break;
                case "Name":
                    buku = buku.OrderBy(s => s.Judul);
                    break;
                case "Date":
                    buku = buku.OrderBy(s => s.Tanggal);
                    break;
                default:
                    buku = buku.OrderByDescending(s => s.Tanggal);
                    break;
            }
            int pageSize = 20;
            return View(await PaginatedList<Buku>.CreateAsync(buku.AsNoTracking(), page ?? 1, pageSize, countItem, countTrash));
        }

        // GET: Buku/Details/5
        [AllowAnonymous]
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

            DetailsBukuViewModel mymodel = new DetailsBukuViewModel();

            mymodel.ArtikelModel = from s in _context.DaftarArtikel
                       .Include(ee => ee.Kategori)
                       .Include(ee => ee.Path)
                       .Take(4)
                                   where s.Status == published
                                   select s;
            mymodel.KonsultasiRepublikaModel = from s in _context.DaftarKonsultasiRepublika
                       .Include(ee => ee.Kategori)
                       .Take(4)
                                               where s.Status == published
                                               select s;

            var atikel = mymodel.ArtikelModel;
            var konsultasiRepublika = mymodel.KonsultasiRepublikaModel;

            if (buku == null)
            {
                return NotFound();
            }

            mymodel.BukuModel = buku;
            return View(mymodel);
        }

        // GET: Buku/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buku/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Judul,Path,FImage,PenulisBuku,Terbitan,ISBN,Deskripsi,Tebal,Tanggal,Kategori,Tag,Penulis,Status")] Buku buku)
        {
            if (ModelState.IsValid)
            {
                buku.Tanggal = DateTime.Now;
                _context.Add(buku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
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

            var buku = await _context.DaftarBuku.Include(ee => ee.Path).SingleOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Path,FImage,PenulisBuku,Terbitan,ISBN,Deskripsi,Tebal,Tanggal,Kategori,Tag,Penulis,Status")] Buku buku)
        {
            if (id != buku.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    buku.Tanggal = DateTime.Now;
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
                return RedirectToAction(nameof(List));
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
            return RedirectToAction(nameof(List));
        }

        private bool BukuExists(int id)
        {
            return _context.DaftarBuku.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Trash(int id)
        {
            var buku = await _context.DaftarBuku.SingleOrDefaultAsync(m => m.Id == id);
            buku.Status = trash;
            _context.DaftarBuku.Update(buku);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Restore(int id)
        {
            var buku = await _context.DaftarBuku.SingleOrDefaultAsync(m => m.Id == id);
            buku.Status = published;
            _context.DaftarBuku.Update(buku);
            await _context.SaveChangesAsync();
            return RedirectToAction("List", new { status = trash });
        }

        public async Task<IActionResult> RemoveCover(int id)
        {
            var buku = await _context.DaftarKonsultasiRumahWasathia.SingleOrDefaultAsync(m => m.Id == id);
            buku.FImage = "";
            _context.DaftarKonsultasiRumahWasathia.Update(buku);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
