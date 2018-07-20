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
using MvcRWV2.Models.KajianAudioViewModels;

namespace MvcRWV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class KajianAudioController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int published = 1;
        private int trash = 2;
        private readonly IHostingEnvironment _hostingEnvironment;
        GoogleDriveFilesRepository driveService;

        public KajianAudioController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            driveService = new GoogleDriveFilesRepository(_hostingEnvironment);
        }

        // GET: KajianAudio
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            string sortOrder,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "" : "date";

            IndexKajianAudioViewModel mymodel = new IndexKajianAudioViewModel();

            var kajianAudio = from s in _context.DaftarKajianAudio
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
                    kajianAudio = kajianAudio.OrderBy(s => s.Tanggal);
                    break;
                default:
                    kajianAudio = kajianAudio.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 15;
            mymodel.KajianAudioModel = await PaginatedList<KajianAudio>.CreateAsync(kajianAudio.AsNoTracking(), page ?? 1, pageSize);
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

            var countItem = (from s in _context.DaftarKajianAudio
                             where s.Status == published
                             select s).Count();
            var countTrash = (from s in _context.DaftarKajianAudio
                              where s.Status == trash
                              select s).Count();

            var kajianAudio = from s in _context.DaftarKajianAudio
                              .Include(ee => ee.Path)
                              .Include(ee => ee.Kategori)
                              select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                kajianAudio = kajianAudio.Where(s => s.Judul.Contains(searchString));
            }

            if (status == null)
            {
                kajianAudio = kajianAudio.Where(s => s.Status == published);
            }
            else if (status == trash)
            {
                kajianAudio = kajianAudio.Where(s => s.Status == trash);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    kajianAudio = kajianAudio.OrderByDescending(s => s.Judul);
                    break;
                case "name":
                    kajianAudio = kajianAudio.OrderBy(s => s.Judul);
                    break;
                case "date":
                    kajianAudio = kajianAudio.OrderBy(s => s.Tanggal);
                    break;
                default:
                    kajianAudio = kajianAudio.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 20;
            return View(await PaginatedList<KajianAudio>.CreateAsync(kajianAudio.AsNoTracking(), page ?? 1, pageSize, countItem, countTrash));
        }

        // GET: KajianAudio/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kajianAudio = await _context.DaftarKajianAudio
                .Include(ee => ee.Path)
                .Include(ee => ee.Kategori)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kajianAudio == null)
            {
                return NotFound();
            }

            return View(kajianAudio);
        }

        // GET: KajianAudio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KajianAudio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?SourceId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Judul,Source,Tanggal,Kategori,Tag,Penulis,Status,Path,Source,FImage,DriveId,Parents")] KajianAudio kajianAudio, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid && file.Length > 0)
                {
                    kajianAudio.Tanggal = DateTime.Now;
                    if (kajianAudio.Source != null)
                    {
                        kajianAudio.Source = kajianAudio.Source.Replace("<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" src=\"", "");
                        kajianAudio.Source = kajianAudio.Source.Replace("\"></iframe>", "");
                    }
                    if (kajianAudio.FImage != null)
                    {
                        kajianAudio.FImage = kajianAudio.FImage.Replace("file/d/", "uc?id=");
                        kajianAudio.FImage = kajianAudio.FImage.Replace("/view?usp=sharing", "");
                    }
                    else
                    {
                        kajianAudio.FImage = "";
                    }
                    if (kajianAudio.Penulis == null)
                    {
                        kajianAudio.Penulis = "admin";
                    }
                    kajianAudio.Status = 1;

                    DriveService service = driveService.GetService();
                    var folderId = "1qzo_cMZ6OTQPvtFSCcSY73_j3tRx_U9W";
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
                           fileMetadata, stream, "video/ogg");
                        request.Fields = "id";
                        request.Upload();
                    }
                    var fileUploaded = request.ResponseBody;
                    kajianAudio.DriveId = fileUploaded.Id;
                    kajianAudio.Source = "https://drive.google.com/uc?id=" + fileUploaded.Id;
                    kajianAudio.Judul = file.FileName;
                    kajianAudio.Parents = folderId;

                    _context.Add(kajianAudio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
                return View(kajianAudio);
            }
            catch
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(kajianAudio);
        }

        // GET: KajianAudio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kajianAudio = await _context.DaftarKajianAudio.Include(ee => ee.Path).SingleOrDefaultAsync(m => m.Id == id);
            if (kajianAudio == null)
            {
                return NotFound();
            }
            return View(kajianAudio);
        }

        // POST: KajianAudio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?SourceId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Source,Tanggal,Kategori,Tag,Penulis,Status,Path,Source,FImage,DriveId,Parents")] KajianAudio kajianAudio)
        {
            if (id != kajianAudio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    kajianAudio.Tanggal = DateTime.Now;
                    kajianAudio.Source = kajianAudio.Source.Replace("<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" src=\"", "");
                    kajianAudio.Source = kajianAudio.Source.Replace("\"></iframe>", "");
                    if (kajianAudio.FImage != null)
                    {
                        kajianAudio.FImage = kajianAudio.FImage.Replace("file/d/", "uc?id=");
                        kajianAudio.FImage = kajianAudio.FImage.Replace("/view?usp=sharing", "");
                    }
                    if (kajianAudio.Penulis == null)
                    {
                        kajianAudio.Penulis = "admin";
                    }
                    _context.Update(kajianAudio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KajianAudioExists(kajianAudio.Id))
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
            return View(kajianAudio);
        }

        // GET: KajianAudio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kajianAudio = await _context.DaftarKajianAudio
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kajianAudio == null)
            {
                return NotFound();
            }

            return View(kajianAudio);
        }

        // POST: KajianAudio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kajianAudio = await _context.DaftarKajianAudio.SingleOrDefaultAsync(m => m.Id == id);
            _context.DaftarKajianAudio.Remove(kajianAudio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool KajianAudioExists(int id)
        {
            return _context.DaftarKajianAudio.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Trash(int id)
        {
            var kajianAudio = await _context.DaftarKajianAudio.SingleOrDefaultAsync(m => m.Id == id);
            kajianAudio.Status = trash;
            _context.DaftarKajianAudio.Update(kajianAudio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Restore(int id)
        {
            var kajianAudio = await _context.DaftarKajianAudio.SingleOrDefaultAsync(m => m.Id == id);
            kajianAudio.Status = published;
            _context.DaftarKajianAudio.Update(kajianAudio);
            await _context.SaveChangesAsync();
            return RedirectToAction("List", new { status = trash });
        }

        public async Task<IActionResult> RemoveCover(int id)
        {
            var kajianAudio = await _context.DaftarKonsultasiRumahWasathia.SingleOrDefaultAsync(m => m.Id == id);
            kajianAudio.FImage = "";
            _context.DaftarKonsultasiRumahWasathia.Update(kajianAudio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
