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
using MvcRWV2.Models.GaleriViewModels;

namespace MvcRWV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class GaleriController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int published = 1;
        private int trash = 2;
        private readonly IHostingEnvironment _hostingEnvironment;
        GoogleDriveFilesRepository driveService;

        public GaleriController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            driveService = new GoogleDriveFilesRepository(_hostingEnvironment);
        }

        // GET: Galeri
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            string sortOrder,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "" : "date";

            IndexGaleriViewModel mymodel = new IndexGaleriViewModel();

            var galeri = from s in _context.DaftarGaleri
                         .Include(ee => ee.Path)
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
            

            switch (sortOrder)
            {
                case "name_desc":
                    galeri = galeri.OrderByDescending(s => s.Judul);
                    break;
                case "name":
                    galeri = galeri.OrderBy(s => s.Judul);
                    break;
                case "date":
                    galeri = galeri.OrderBy(s => s.Tanggal);
                    break;
                default:
                    galeri = galeri.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 15;
            mymodel.GaleriModel = await PaginatedList<Galeri>.CreateAsync(galeri.AsNoTracking(), page ?? 1, pageSize);
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
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "" : "date";
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

            var galeri = from s in _context.DaftarGaleri
                         .Include(ee => ee.Path)
                         .Include(ee => ee.Kategori)
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                galeri = galeri.Where(s => s.Judul.Contains(searchString));
            }

            if (status == null)
            {
                galeri = galeri.Where(s => s.Status == published);
            }
            else if (status == trash)
            {
                galeri = galeri.Where(s => s.Status == trash);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    galeri = galeri.OrderByDescending(s => s.Judul);
                    break;
                case "name":
                    galeri = galeri.OrderBy(s => s.Judul);
                    break;
                case "date":
                    galeri = galeri.OrderBy(s => s.Tanggal);
                    break;
                default:
                    galeri = galeri.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 20;
            return View(await PaginatedList<Galeri>.CreateAsync(galeri.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Galeri/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetailsGaleriViewModel mymodel = new DetailsGaleriViewModel();

            var galeri = await _context.DaftarGaleri
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
            var sourceImage = galeri.Source.Split(
                            new[] { "\r\n", "\r", "\n" },
                            StringSplitOptions.None
                        );
            if (galeri == null)
            {
                return NotFound();
            }
            mymodel.GaleriModel = galeri;
            mymodel.SourceImage = sourceImage;
            return View(mymodel);
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
        public async Task<IActionResult> Create([Bind("Id,Judul,Source,FImage,Tanggal,Path,Kategori,Tag,Status,Jumlah,Penulis,DriveId,Parents")] Galeri galeri, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                galeri.Tanggal = DateTime.Now;
                if (galeri.FImage != null)
                {
                    galeri.FImage = galeri.FImage.Replace("file/d/", "uc?id=");
                    galeri.FImage = galeri.FImage.Replace("/view?usp=sharing", "");
                }
                if (galeri.Penulis == null)
                {
                    galeri.Penulis = "admin";
                }
                galeri.Status = 1;
                galeri.Jumlah = galeri.Source.Length;
                _context.Add(galeri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Source,FImage,Tanggal,Path,Kategori,Tag,Status,Jumlah,Penulis,DriveId,Parents")] Galeri galeri)
        {
            if (id != galeri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    galeri.Tanggal = DateTime.Now;
                    if (galeri.FImage != null)
                    {
                        galeri.FImage = galeri.FImage.Replace("file/d/", "uc?id=");
                        galeri.FImage = galeri.FImage.Replace("/view?usp=sharing", "");
                    }
                    if (galeri.Penulis == null)
                    {
                        galeri.Penulis = "admin";
                    }
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
                return RedirectToAction(nameof(List));
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
            return RedirectToAction(nameof(List));
        }

        private bool GaleriExists(int id)
        {
            return _context.DaftarGaleri.Any(e => e.Id == id);
        }
    }
}
