﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IHostingEnvironment _hostingEnvironment;
        GoogleDriveFilesRepository driveService;

        public BukuController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            driveService = new GoogleDriveFilesRepository(_hostingEnvironment);
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
                case "name":
                    buku = buku.OrderBy(s => s.Judul);
                    break;
                case "date":
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
                case "name":
                    buku = buku.OrderBy(s => s.Judul);
                    break;
                case "date":
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
        public async Task<IActionResult> Create([Bind("Id,Judul,Path,FImage,PenulisBuku,Terbitan,ISBN,Deskripsi,Tebal,Tanggal,Kategori,Tag,Penulis,Status,DriveId,Parents")] Buku buku, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                buku.Tanggal = DateTime.Now;
                if (buku.FImage != null)
                {
                    buku.FImage = buku.FImage.Replace("file/d/", "uc?id=");
                    buku.FImage = buku.FImage.Replace("/view?usp=sharing", "");
                }
                if (buku.Penulis == null)
                {
                    buku.Penulis = "admin";
                }
                buku.Status = 1;

                DriveService service = driveService.GetService();
                var folderId = "1aB_0pJ9qsHjP3DhOERmWacA2Mn1jDW7H";
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
                       fileMetadata, stream, "image/jpeg");
                    request.Fields = "id";
                    request.Upload();
                }
                var fileUploaded = request.ResponseBody;
                buku.DriveId = fileUploaded.Id;
                buku.Source = "https://drive.google.com/uc?id=" + fileUploaded.Id;
                buku.FImage = "https://drive.google.com/uc?id=" + fileUploaded.Id;
                buku.Judul = file.FileName;
                buku.Parents = folderId;

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Path,FImage,PenulisBuku,Terbitan,ISBN,Deskripsi,Tebal,Tanggal,Kategori,Tag,Penulis,Status,DriveId,Parents")] Buku buku, IFormFile file)
        {
            if (id != buku.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DriveService service = driveService.GetService();
                    var folderId = "1aB_0pJ9qsHjP3DhOERmWacA2Mn1jDW7H";
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
                           fileMetadata, stream, "image/jpeg");
                        request.Fields = "id";
                        request.Upload();
                    }
                    var fileUploaded = request.ResponseBody;
                    buku.FImage = "https://drive.google.com/uc?id=" + fileUploaded.Id;

                    if (buku.FImage != null)
                    {
                        buku.FImage = buku.FImage.Replace("file/d/", "uc?id=");
                        buku.FImage = buku.FImage.Replace("/view?usp=sharing", "");
                    }
                    if (buku.Penulis == null)
                    {
                        buku.Penulis = "admin";
                    }
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
        public async Task<IActionResult> DeleteConfirmed(int id, string driveId)
        {
            var buku = await _context.DaftarBuku.SingleOrDefaultAsync(m => m.Id == id);
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
            var buku = await _context.DaftarBuku.SingleOrDefaultAsync(m => m.Id == id);
            buku.FImage = "";
            _context.DaftarBuku.Update(buku);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
