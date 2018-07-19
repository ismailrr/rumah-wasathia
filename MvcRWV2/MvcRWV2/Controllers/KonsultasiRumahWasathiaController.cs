using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcRWV2.Data;
using MvcRWV2.Models;
using MvcRWV2.Models.KonsultasiRumahWasathiaViewModels;

namespace MvcRWV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class KonsultasiRumahWasathiaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int published = 1;
        private int trash = 2;
        private readonly IHostingEnvironment _hostingEnvironment;
        GoogleDriveFilesRepository driveService;

        public KonsultasiRumahWasathiaController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            driveService = new GoogleDriveFilesRepository(_hostingEnvironment);
        }

        // GET: KonsultasiRumahWasathia
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "" : "date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            IndexKonsultasiRWViewModel mymodel = new IndexKonsultasiRWViewModel();

            var konsultasiRumahWasathia = from s in _context.DaftarKonsultasiRumahWasathia
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
                konsultasiRumahWasathia = konsultasiRumahWasathia.Where(s => s.Judul.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    konsultasiRumahWasathia = konsultasiRumahWasathia.OrderByDescending(s => s.Judul);
                    break;
                case "name":
                    konsultasiRumahWasathia = konsultasiRumahWasathia.OrderBy(s => s.Judul);
                    break;
                case "date":
                    konsultasiRumahWasathia = konsultasiRumahWasathia.OrderBy(s => s.Tanggal);
                    break;
                default:
                    konsultasiRumahWasathia = konsultasiRumahWasathia.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 12;
            mymodel.KonsultasiRumahWasathiaModel = await PaginatedList<KonsultasiRumahWasathia>.CreateAsync(konsultasiRumahWasathia.AsNoTracking(), page ?? 1, pageSize);
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

            var countItem = (from s in _context.DaftarKonsultasiRumahWasathia
                             where s.Status == published
                             select s).Count();
            var countTrash = (from s in _context.DaftarKonsultasiRumahWasathia
                              where s.Status == trash
                              select s).Count();

            var konsultasiRumahWasathia = from s in _context.DaftarKonsultasiRumahWasathia
                          .Include(ee => ee.Path)
                          .Include(ee => ee.Kategori)
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                konsultasiRumahWasathia = konsultasiRumahWasathia.Where(s => s.Judul.Contains(searchString));
            }

            if (status == null)
            {
                konsultasiRumahWasathia = konsultasiRumahWasathia.Where(s => s.Status == published);
            }
            else if (status == trash)
            {
                konsultasiRumahWasathia = konsultasiRumahWasathia.Where(s => s.Status == trash);
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

            int pageSize = 20;
            return View(await PaginatedList<KonsultasiRumahWasathia>.CreateAsync(konsultasiRumahWasathia.AsNoTracking(), page ?? 1, pageSize, countItem, countTrash));
        }

        // GET: KonsultasiRumahWasathia/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetailsKonsultasiRWViewModel mymodel = new DetailsKonsultasiRWViewModel();

            var konsultasiRumahWasathia = await _context.DaftarKonsultasiRumahWasathia
                .Include(ee => ee.Kategori)
                .SingleOrDefaultAsync(m => m.Id == id);

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

            if (konsultasiRumahWasathia == null)
            {
                return NotFound();
            }

            mymodel.KonsultasiRumahWasathiaModel = konsultasiRumahWasathia;
            return View(mymodel);
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
        public async Task<IActionResult> Create([Bind("Id,PenulisKonten,Judul,Tanggal,Pertanyaan,Jawaban,Path,FImage,Kategori,Tag,Penulis,Status,DriveId,Parents")] KonsultasiRumahWasathia konsultasiRumahWasathia, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                konsultasiRumahWasathia.Tanggal = DateTime.Now;
                if (konsultasiRumahWasathia.FImage != null)
                {
                    konsultasiRumahWasathia.FImage = konsultasiRumahWasathia.FImage.Replace("file/d/", "uc?id=");
                    konsultasiRumahWasathia.FImage = konsultasiRumahWasathia.FImage.Replace("/view?usp=sharing", "");
                }
                if (konsultasiRumahWasathia.PenulisKonten == null)
                {
                    konsultasiRumahWasathia.PenulisKonten = "admin";
                }
                konsultasiRumahWasathia.Status = 1;
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

            var konsultasiRumahWasathia = await _context.DaftarKonsultasiRumahWasathia.Include(ee => ee.Path).SingleOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,PenulisKonten,Judul,Tanggal,Pertanyaan,Jawaban,Path,FImage,Kategori,Tag,Penulis,Status,DriveId,Parents")] KonsultasiRumahWasathia konsultasiRumahWasathia)
        {
            if (id != konsultasiRumahWasathia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    konsultasiRumahWasathia.Tanggal = DateTime.Now;
                    if (konsultasiRumahWasathia.FImage != null)
                    {
                        konsultasiRumahWasathia.FImage = konsultasiRumahWasathia.FImage.Replace("file/d/", "uc?id=");
                        konsultasiRumahWasathia.FImage = konsultasiRumahWasathia.FImage.Replace("/view?usp=sharing", "");
                    }
                    if (konsultasiRumahWasathia.PenulisKonten == null)
                    {
                        konsultasiRumahWasathia.PenulisKonten = "admin";
                    }
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
                return RedirectToAction(nameof(List));
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

        public async Task<IActionResult> Trash(int id)
        {
            var konsultasiRumahWasathia = await _context.DaftarKonsultasiRumahWasathia.SingleOrDefaultAsync(m => m.Id == id);
            konsultasiRumahWasathia.Status = trash;
            _context.DaftarKonsultasiRumahWasathia.Update(konsultasiRumahWasathia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Restore(int id)
        {
            var konsultasiRumahWasathia = await _context.DaftarKonsultasiRumahWasathia.SingleOrDefaultAsync(m => m.Id == id);
            konsultasiRumahWasathia.Status = published;
            _context.DaftarKonsultasiRumahWasathia.Update(konsultasiRumahWasathia);
            await _context.SaveChangesAsync();
            return RedirectToAction("List", new { status = trash });
        }

        public async Task<IActionResult> RemoveCover(int id)
        {
            var konsultasiRumahWasathia = await _context.DaftarKonsultasiRumahWasathia.SingleOrDefaultAsync(m => m.Id == id);
            konsultasiRumahWasathia.FImage = "";
            _context.DaftarKonsultasiRumahWasathia.Update(konsultasiRumahWasathia);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { Id = id });
        }
    }
}
