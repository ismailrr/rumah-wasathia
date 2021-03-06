﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcRWV2.Data;
using MvcRWV2.Models;
using MvcRWV2.Models.ArtikelViewModels;

namespace MvcRWV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ArtikelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int published = 1;
        private int trash = 2;
        private readonly IHostingEnvironment _hostingEnvironment;
        GoogleDriveFilesRepository driveService;
        
        public ArtikelController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            driveService = new GoogleDriveFilesRepository(_hostingEnvironment);
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

            IndexArtikelViewModel mymodel = new IndexArtikelViewModel();

            var artikel = from s in _context.DaftarArtikel
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
            mymodel.KonsultasiRepublikaModel = from s in _context.DaftarKonsultasiRepublika
                       .Include(ee => ee.Kategori)
                       .Take(4)
                       where s.Status == published
                       select s;

            var buku = mymodel.BukuModel;
            var konsultasiRepublika = mymodel.KonsultasiRepublikaModel;

            if (!String.IsNullOrEmpty(searchString))
            {
                artikel = artikel.Where(s => s.Judul.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    artikel = artikel.OrderByDescending(s => s.Judul);
                    break;
                case "name":
                    artikel = artikel.OrderBy(s => s.Judul);
                    break;
                case "date":
                    artikel = artikel.OrderBy(s => s.Tanggal);
                    break;
                default:
                    artikel = artikel.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 12;
            mymodel.ArtikelModel = await PaginatedList<Artikel>.CreateAsync(artikel.AsNoTracking(), page ?? 1, pageSize);
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
                case "name":
                    artikel = artikel.OrderBy(s => s.Judul);
                    break;
                case "date":
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
        public async Task<IActionResult> Create([Bind("Id,Judul,Tanggal,Path,Source,FImage,Kategori,Tag,Penulis,Status,DriveId,Parents")] Artikel artikel, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid && file.Length > 0)
                {
                    artikel.Tanggal = DateTime.Now;
                    if (artikel.FImage != null)
                    {
                        artikel.FImage = artikel.FImage.Replace("file/d/", "uc?id=");
                        artikel.FImage = artikel.FImage.Replace("/view?usp=sharing", "");
                    }
                    if (artikel.FImage == null)
                    {
                        artikel.FImage = "/uploads/image/general/chart.png";
                    }
                    if (artikel.Penulis == null)
                    {
                        artikel.Penulis = "admin";
                    }
                    artikel.Status = 1;

                    DriveService service = driveService.GetService();
                    var folderId = "1MeEImyGO6ma6mn9m-UiiNDNb9OX_F63S";
                    string path = Path.GetTempFileName();
                    var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                    {
                        Name = Path.GetFileName(file.FileName),
                        Parents = new List<string>
                        {
                            folderId
                        }
                    };
                    FilesResource.CreateMediaUpload request;
   
                    using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                    {
                         await file.CopyToAsync(stream);
                         request = service.Files.Create(
                            fileMetadata, stream, "application/pdf");
                         request.Fields = "id";
                         request.Upload();
                    }
                    var fileUploaded = request.ResponseBody;
                    artikel.DriveId = fileUploaded.Id;
                    artikel.Source = "https://drive.google.com/uc?id=" + fileUploaded.Id;
                    artikel.Judul = file.FileName;
                    artikel.Parents = folderId;
                    
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Tanggal,Path,Source,FImage,Kategori,Tag,Penulis,Status,DriveId,Parents")] Artikel artikel)
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
                    if (artikel.FImage != null)
                    {
                        artikel.FImage = artikel.FImage.Replace("file/d/", "uc?id=");
                        artikel.FImage = artikel.FImage.Replace("/view?usp=sharing", "");
                    }
                    if (artikel.Penulis == null)
                    {
                        artikel.Penulis = "admin";
                    }
                    _context.Update(artikel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(List));
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
        public async Task<IActionResult> DeleteConfirmed(int id,string driveId)
        {
            var artikel = await _context.DaftarArtikel.SingleOrDefaultAsync(m => m.Id == id);
            DriveService service = driveService.GetService();
            try
            {
                // Initial validation.
                if (service == null)
                    throw new ArgumentNullException("service");

                if (driveId != null)
                    service.Files.Delete(driveId).Execute();

            }
            catch (Exception ex)
            {
                throw new Exception("Request Files.Delete failed.", ex);
            }
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

        public async Task<IActionResult> RemoveCover(int id)
        {
            var artikel = await _context.DaftarArtikel.SingleOrDefaultAsync(m => m.Id == id);
            artikel.FImage = "";
            _context.DaftarArtikel.Update(artikel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { Id = id });
        }
    }
}
