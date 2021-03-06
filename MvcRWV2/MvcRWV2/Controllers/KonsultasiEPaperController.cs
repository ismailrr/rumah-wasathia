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
using MvcRWV2.Models.KonsultasiEPaperViewModels;

namespace MvcRWV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class KonsultasiEPaperController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int published = 1;
        private int trash = 2;
        private readonly IHostingEnvironment _hostingEnvironment;
        GoogleDriveFilesRepository driveService;

        public KonsultasiEPaperController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            driveService = new GoogleDriveFilesRepository(_hostingEnvironment);
        }

        // GET: KonsultasiEPaper
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            string sortOrder,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "" : "date";

            IndexKonsultasiEPaperViewModel mymodel = new IndexKonsultasiEPaperViewModel();

            var konsultasiEPaper = from s in _context.DaftarKonsultasiEPaper
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
                    konsultasiEPaper = konsultasiEPaper.OrderByDescending(s => s.Judul);
                    break;
                case "name":
                    konsultasiEPaper = konsultasiEPaper.OrderBy(s => s.Judul);
                    break;
                case "date":
                    konsultasiEPaper = konsultasiEPaper.OrderBy(s => s.Tanggal);
                    break;
                default:
                    konsultasiEPaper = konsultasiEPaper.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 15;
            mymodel.KonsultasiEPaperModel = await PaginatedList<KonsultasiEPaper>.CreateAsync(konsultasiEPaper.AsNoTracking(), page ?? 1, pageSize);
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

            var countItem = (from s in _context.DaftarKonsultasiEPaper
                             where s.Status == published
                             select s).Count();
            var countTrash = (from s in _context.DaftarKonsultasiEPaper
                              where s.Status == trash
                              select s).Count();

            var konsultasiEPaper = from s in _context.DaftarKonsultasiEPaper
                          .Include(ee => ee.Path)
                          .Include(ee => ee.Kategori)
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                konsultasiEPaper = konsultasiEPaper.Where(s => s.Judul.Contains(searchString));
            }

            if (status == null)
            {
                konsultasiEPaper = konsultasiEPaper.Where(s => s.Status == published);
            }
            else if (status == trash)
            {
                konsultasiEPaper = konsultasiEPaper.Where(s => s.Status == trash);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    konsultasiEPaper = konsultasiEPaper.OrderByDescending(s => s.Judul);
                    break;
                case "name":
                    konsultasiEPaper = konsultasiEPaper.OrderBy(s => s.Judul);
                    break;
                case "date":
                    konsultasiEPaper = konsultasiEPaper.OrderBy(s => s.Tanggal);
                    break;
                default:
                    konsultasiEPaper = konsultasiEPaper.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 20;
            return View(await PaginatedList<KonsultasiEPaper>.CreateAsync(konsultasiEPaper.AsNoTracking(), page ?? 1, pageSize, countItem, countTrash));
        }

        // GET: KonsultasiEPaper/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konsultasiEPaper = await _context.DaftarKonsultasiEPaper
                .Include(ee => ee.Path)
                .Include(ee => ee.Kategori)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (konsultasiEPaper == null)
            {
                return NotFound();
            }

            return View(konsultasiEPaper);
        }

        // GET: KonsultasiEPaper/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KonsultasiEPaper/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Judul,Tanggal,Path,Source,FImage,Kategori,Tag,Penulis,Status,DriveId,Parents")] KonsultasiEPaper konsultasiEPaper, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                konsultasiEPaper.Tanggal = DateTime.Now;
                if (konsultasiEPaper.FImage != null)
                {
                    konsultasiEPaper.FImage = konsultasiEPaper.FImage.Replace("file/d/", "uc?id=");
                    konsultasiEPaper.FImage = konsultasiEPaper.FImage.Replace("/view?usp=sharing", "");
                }
                if (konsultasiEPaper.Penulis == null)
                {
                    konsultasiEPaper.Penulis = "admin";
                }
                konsultasiEPaper.Status = 1;

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
                konsultasiEPaper.DriveId = fileUploaded.Id;
                konsultasiEPaper.Source = "https://drive.google.com/uc?id=" + fileUploaded.Id;
                konsultasiEPaper.FImage = "https://drive.google.com/uc?id=" + fileUploaded.Id;
                konsultasiEPaper.Judul = file.FileName;
                konsultasiEPaper.Parents = folderId;

                _context.Add(konsultasiEPaper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(konsultasiEPaper);
        }

        // GET: KonsultasiEPaper/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konsultasiEPaper = await _context.DaftarKonsultasiEPaper.Include(ee => ee.Path).SingleOrDefaultAsync(m => m.Id == id);
            if (konsultasiEPaper == null)
            {
                return NotFound();
            }
            return View(konsultasiEPaper);
        }

        // POST: KonsultasiEPaper/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Tanggal,Path,Source,FImage,Kategori,Tag,Penulis,Status,DriveId,Parents")] KonsultasiEPaper konsultasiEPaper)
        {
            if (id != konsultasiEPaper.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    konsultasiEPaper.Tanggal = DateTime.Now;
                    if (konsultasiEPaper.FImage != null)
                    {
                        konsultasiEPaper.FImage = konsultasiEPaper.FImage.Replace("file/d/", "uc?id=");
                        konsultasiEPaper.FImage = konsultasiEPaper.FImage.Replace("/view?usp=sharing", "");
                    }
                    if (konsultasiEPaper.Penulis == null)
                    {
                        konsultasiEPaper.Penulis = "admin";
                    }
                    _context.Update(konsultasiEPaper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KonsultasiEPaperExists(konsultasiEPaper.Id))
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
            return View(konsultasiEPaper);
        }

        // GET: KonsultasiEPaper/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konsultasiEPaper = await _context.DaftarKonsultasiEPaper
                .SingleOrDefaultAsync(m => m.Id == id);
            if (konsultasiEPaper == null)
            {
                return NotFound();
            }

            return View(konsultasiEPaper);
        }

        // POST: KonsultasiEPaper/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string driveId)
        {
            var konsultasiEPaper = await _context.DaftarKonsultasiEPaper.SingleOrDefaultAsync(m => m.Id == id);
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
            _context.DaftarKonsultasiEPaper.Remove(konsultasiEPaper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool KonsultasiEPaperExists(int id)
        {
            return _context.DaftarKonsultasiEPaper.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Trash(int id)
        {
            var konsultasiEPaper = await _context.DaftarKonsultasiEPaper.SingleOrDefaultAsync(m => m.Id == id);
            konsultasiEPaper.Status = trash;
            _context.DaftarKonsultasiEPaper.Update(konsultasiEPaper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Restore(int id)
        {
            var konsultasiEPaper = await _context.DaftarKonsultasiEPaper.SingleOrDefaultAsync(m => m.Id == id);
            konsultasiEPaper.Status = published;
            _context.DaftarKonsultasiEPaper.Update(konsultasiEPaper);
            await _context.SaveChangesAsync();
            return RedirectToAction("List", new { status = trash });
        }

        public async Task<IActionResult> RemoveCover(int id)
        {
            var kosultasiEPaper = await _context.DaftarKonsultasiEPaper.SingleOrDefaultAsync(m => m.Id == id);
            kosultasiEPaper.FImage = "";
            _context.DaftarKonsultasiEPaper.Update(kosultasiEPaper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
