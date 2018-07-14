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
    public class KonsultasiEPaperController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int published = 1;
        private int trash = 2;

        public KonsultasiEPaperController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KonsultasiEPaper
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            string sortOrder,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "" : "Date";

            var konsultasiEPaper = from s in _context.DaftarKonsultasiEPaper
                                   .Include(ee => ee.Path)
                                   .Include(ee => ee.Kategori)
                                   where s.Status == published
                                   select s;

            switch (sortOrder)
            {
                case "name_desc":
                    konsultasiEPaper = konsultasiEPaper.OrderByDescending(s => s.Judul);
                    break;
                case "Name":
                    konsultasiEPaper = konsultasiEPaper.OrderBy(s => s.Judul);
                    break;
                case "Date":
                    konsultasiEPaper = konsultasiEPaper.OrderBy(s => s.Tanggal);
                    break;
                default:
                    konsultasiEPaper = konsultasiEPaper.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 15;
            return View(await PaginatedList<KonsultasiEPaper>.CreateAsync(konsultasiEPaper.AsNoTracking(), page ?? 1, pageSize));
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
                case "Name":
                    konsultasiEPaper = konsultasiEPaper.OrderBy(s => s.Judul);
                    break;
                case "Date":
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
        public async Task<IActionResult> Create([Bind("Id,Judul,Tanggal,Path,Source,FImage,Kategori,Tag,Penulis,Status")] KonsultasiEPaper konsultasiEPaper)
        {
            if (ModelState.IsValid)
            {
                konsultasiEPaper.Tanggal = DateTime.Now;
                _context.Add(konsultasiEPaper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Tanggal,Path,Source,FImage,Kategori,Tag,Penulis,Status")] KonsultasiEPaper konsultasiEPaper)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var konsultasiEPaper = await _context.DaftarKonsultasiEPaper.SingleOrDefaultAsync(m => m.Id == id);
            _context.DaftarKonsultasiEPaper.Remove(konsultasiEPaper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
            var kosultasiEPaper = await _context.DaftarKonsultasiRumahWasathia.SingleOrDefaultAsync(m => m.Id == id);
            kosultasiEPaper.FImage = "";
            _context.DaftarKonsultasiRumahWasathia.Update(kosultasiEPaper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
