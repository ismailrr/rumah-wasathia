using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using MvcRW.Data;
using MvcRW.Models;
using MvcRW.ViewModels;

namespace MvcRW.Controllers
{
    public class PathArtikelController : Controller
    {
        private readonly RWContext _context;
        private readonly IHostingEnvironment _environment;

        public PathArtikelController(RWContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: PathArtikel
        public async Task<IActionResult> Index(
            string sortOrder,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            var pathArtikel = from s in _context.DaftarPathArtikel
                             select s;
            switch (sortOrder)
            {
                case "Date":
                    pathArtikel = pathArtikel.OrderBy(s => s.Tanggal);
                    break;
                default:
                    pathArtikel = pathArtikel.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 12;
            return View(await PaginatedList<PathArtikel>.CreateAsync(pathArtikel.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: PathArtikel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pathArtikel = await _context.DaftarPathArtikel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pathArtikel == null)
            {
                return NotFound();
            }

            return View(pathArtikel);
        }

        // GET: PathArtikel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PathArtikel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] PathArtikelVM pathArtikelVM)
        {
            if (ModelState.IsValid)
            {
                //Directory.GetCurrentDirectory(),
                var filePath = Path.Combine("Uploads", pathArtikelVM.Path.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await pathArtikelVM.Path.CopyToAsync(stream);
                }

                PathArtikel pathArtikel = new PathArtikel() 
                {
                    Path = pathArtikelVM.Path.FileName,
                    Tanggal = DateTime.Now
                };

                _context.Add(pathArtikel);
                await _context.SaveChangesAsync();

                //Message = $"Upload document {UploadFile.FilePath} has been successfully!";

                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(pathArtikelVM);
        }

        // GET: PathArtikel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pathArtikel = await _context.DaftarPathArtikel.SingleOrDefaultAsync(m => m.Id == id);
            if (pathArtikel == null)
            {
                return NotFound();
            }
            return View(pathArtikel);
        }

        // POST: PathArtikel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Path,Tanggal")] PathArtikel pathArtikel)
        {
            if (id != pathArtikel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pathArtikel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PathArtikelExists(pathArtikel.Id))
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
            return View(pathArtikel);
        }

        // GET: PathArtikel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pathArtikel = await _context.DaftarPathArtikel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pathArtikel == null)
            {
                return NotFound();
            }

            return View(pathArtikel);
        }

        // POST: PathArtikel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pathArtikel = await _context.DaftarPathArtikel.SingleOrDefaultAsync(m => m.Id == id);
            _context.DaftarPathArtikel.Remove(pathArtikel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PathArtikelExists(int id)
        {
            return _context.DaftarPathArtikel.Any(e => e.Id == id);
        }
    }
}
