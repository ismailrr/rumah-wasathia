using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcRW.Data;
using MvcRW.Models;

namespace MvcRW.Controllers
{
    public class KonsultasiInfografisController : Controller
    {
        private readonly RWContext _context;

        public KonsultasiInfografisController(RWContext context)
        {
            _context = context;
        }

        // GET: Infografis
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var infografis = from s in _context.DaftarKonsultasiInfografis
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                infografis = infografis.Where(s => s.Judul.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    infografis = infografis.OrderByDescending(s => s.Judul);
                    break;
                case "Date":
                    infografis = infografis.OrderBy(s => s.Tanggal);
                    break;
                case "date_desc":
                    infografis = infografis.OrderByDescending(s => s.Tanggal);
                    break;
                default:
                    infografis = infografis.OrderBy(s => s.Judul);
                    break;
            }

            int pageSize = 12;
            return View(await PaginatedList<KonsultasiInfografis>.CreateAsync(infografis.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Infografis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infografis = await _context.DaftarKonsultasiInfografis
                .SingleOrDefaultAsync(m => m.Id == id);
            if (infografis == null)
            {
                return NotFound();
            }

            return View(infografis);
        }

        // GET: Infografis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Infografis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Judul,Tanggal")] KonsultasiInfografis infografis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(infografis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(infografis);
        }

        // GET: Infografis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infografis = await _context.DaftarKonsultasiInfografis.SingleOrDefaultAsync(m => m.Id == id);
            if (infografis == null)
            {
                return NotFound();
            }
            return View(infografis);
        }

        // POST: Infografis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Tanggal")] KonsultasiInfografis infografis)
        {
            if (id != infografis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(infografis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfografisExists(infografis.Id))
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
            return View(infografis);
        }

        // GET: Infografis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infografis = await _context.DaftarKonsultasiInfografis
                .SingleOrDefaultAsync(m => m.Id == id);
            if (infografis == null)
            {
                return NotFound();
            }

            return View(infografis);
        }

        // POST: Infografis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var infografis = await _context.DaftarKonsultasiInfografis.SingleOrDefaultAsync(m => m.Id == id);
            _context.DaftarKonsultasiInfografis.Remove(infografis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfografisExists(int id)
        {
            return _context.DaftarKonsultasiInfografis.Any(e => e.Id == id);
        }
    }
}
