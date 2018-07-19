using System;
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
using MvcRWV2.Models.KajianVideoViewModels;

namespace MvcRWV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class KajianVideoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int published = 1;
        private int trash = 2;
        private readonly IHostingEnvironment _hostingEnvironment;
        GoogleDriveFilesRepository driveService;

        public KajianVideoController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            driveService = new GoogleDriveFilesRepository(_hostingEnvironment);
        }

        // GET: KajianVideo
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            string sortOrder,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "" : "date";

            IndexKajianVideoViewModel mymodel = new IndexKajianVideoViewModel();

            var kajianVideo = from s in _context.DaftarKajianVideo
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
                case "date":
                    kajianVideo = kajianVideo.OrderBy(s => s.Tanggal);
                    break;
                default:
                    kajianVideo = kajianVideo.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 12;
            mymodel.KajianVideoModel = await PaginatedList<KajianVideo>.CreateAsync(kajianVideo.AsNoTracking(), page ?? 1, pageSize);
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

            var countItem = (from s in _context.DaftarKajianVideo
                             where s.Status == published
                             select s).Count();
            var countTrash = (from s in _context.DaftarKajianVideo
                              where s.Status == trash
                              select s).Count();

            var kajianVideo = from s in _context.DaftarKajianVideo
                              .Include(ee => ee.Path)
                              .Include(ee => ee.Kategori)
                              select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                kajianVideo = kajianVideo.Where(s => s.Judul.Contains(searchString));
            }

            if (status == null)
            {
                kajianVideo = kajianVideo.Where(s => s.Status == published);
            }
            else if (status == trash)
            {
                kajianVideo = kajianVideo.Where(s => s.Status == trash);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    kajianVideo = kajianVideo.OrderByDescending(s => s.Judul);
                    break;
                case "name":
                    kajianVideo = kajianVideo.OrderBy(s => s.Judul);
                    break;
                case "date":
                    kajianVideo = kajianVideo.OrderBy(s => s.Tanggal);
                    break;
                default:
                    kajianVideo = kajianVideo.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 20;
            return View(await PaginatedList<KajianVideo>.CreateAsync(kajianVideo.AsNoTracking(), page ?? 1, pageSize, countItem, countTrash));
        }

        // GET: KajianVideo/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kajianVideo = await _context.DaftarKajianVideo
                .Include(ee => ee.Path)
                .Include(ee => ee.Kategori)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kajianVideo == null)
            {
                return NotFound();
            }

            return View(kajianVideo);
        }

        // GET: KajianVideo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KajianVideo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?SourceId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Judul,Source,Tanggal,Kategori,Tag,Penulis,Status,Path,Source,FImage,DriveId,Parents")] KajianVideo kajianVideo, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                kajianVideo.Tanggal = DateTime.Now;
                if (kajianVideo.Source != null)
                {
                    kajianVideo.Source = kajianVideo.Source.Replace("watch?v=", "embed/");
                }
                if (kajianVideo.FImage != null)
                {
                    kajianVideo.FImage = kajianVideo.FImage.Replace("file/d/", "uc?id=");
                    kajianVideo.FImage = kajianVideo.FImage.Replace("/view?usp=sharing", "");
                }
                else
                {
                    kajianVideo.FImage = "";
                }
                if (kajianVideo.Penulis == null)
                {
                    kajianVideo.Penulis = "admin";
                }
                kajianVideo.Status = 1;

                DriveService service = driveService.GetService();
                var folderId = "1baYXzsU1YKek7HMSq9NDCyiP9jqJ4cpu";
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
                       fileMetadata, stream, "video/mp4");
                    request.Fields = "id";
                    request.Upload();
                }
                var fileUploaded = request.ResponseBody;
                kajianVideo.DriveId = fileUploaded.Id;
                kajianVideo.Source = "https://drive.google.com/uc?id=" + fileUploaded.Id;
                kajianVideo.Judul = file.FileName;
                kajianVideo.Parents = folderId;

                _context.Add(kajianVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(kajianVideo);
        }

        // GET: KajianVideo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kajianVideo = await _context.DaftarKajianVideo.Include(ee => ee.Path).SingleOrDefaultAsync(m => m.Id == id);
            if (kajianVideo == null)
            {
                return NotFound();
            }
            return View(kajianVideo);
        }

        // POST: KajianVideo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?SourceId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Source,Tanggal,Kategori,Tag,Penulis,Status,Path,Source,FImage,DriveId,Parents")] KajianVideo kajianVideo)
        {
            if (id != kajianVideo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    kajianVideo.Tanggal = DateTime.Now;
                    kajianVideo.Source = kajianVideo.Source.Replace("watch?v=", "embed/");
                    if (kajianVideo.FImage != null)
                    {
                        kajianVideo.FImage = kajianVideo.FImage.Replace("file/d/", "uc?id=");
                        kajianVideo.FImage = kajianVideo.FImage.Replace("/view?usp=sharing", "");
                    }
                    if (kajianVideo.Penulis == null)
                    {
                        kajianVideo.Penulis = "admin";
                    }
                    _context.Update(kajianVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KajianVideoExists(kajianVideo.Id))
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
            return View(kajianVideo);
        }

        // GET: KajianVideo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kajianVideo = await _context.DaftarKajianVideo
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kajianVideo == null)
            {
                return NotFound();
            }

            return View(kajianVideo);
        }

        // POST: KajianVideo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kajianVideo = await _context.DaftarKajianVideo.SingleOrDefaultAsync(m => m.Id == id);
            _context.DaftarKajianVideo.Remove(kajianVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool KajianVideoExists(int id)
        {
            return _context.DaftarKajianVideo.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Trash(int id)
        {
            var kajianVideo = await _context.DaftarKajianVideo.SingleOrDefaultAsync(m => m.Id == id);
            kajianVideo.Status = trash;
            _context.DaftarKajianVideo.Update(kajianVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Restore(int id)
        {
            var kajianVideo = await _context.DaftarKajianVideo.SingleOrDefaultAsync(m => m.Id == id);
            kajianVideo.Status = published;
            _context.DaftarKajianVideo.Update(kajianVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction("List", new { status = trash });
        }

        public async Task<IActionResult> RemoveCover(int id)
        {
            var kajianVideo = await _context.DaftarKonsultasiRumahWasathia.SingleOrDefaultAsync(m => m.Id == id);
            kajianVideo.FImage = "";
            _context.DaftarKonsultasiRumahWasathia.Update(kajianVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
