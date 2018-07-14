﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcRWV2.Data;
using MvcRWV2.Models;
using MvcRWV2.Models.KonsultasiInfografisViewModels;

namespace MvcRWV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class KonsultasiInfografisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int published = 1;
        private int trash = 2;

        public KonsultasiInfografisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Infografis
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            IndexKonsultasiInfografisViewModel mymodel = new IndexKonsultasiInfografisViewModel();

            var infografis = from s in _context.DaftarKonsultasiInfografis
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
            mymodel.BukuModel = from s in _context.DaftarBuku
                       .Include(ee => ee.Kategori)
                       .Include(ee => ee.Path)
                       .Take(4)
                                where s.Status == published
                                select s;

            var atikel = mymodel.ArtikelModel;
            var buku = mymodel.BukuModel;

            if (!String.IsNullOrEmpty(searchString))
            {
                infografis = infografis.Where(s => s.Judul.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    infografis = infografis.OrderByDescending(s => s.Judul);
                    break;
                case "Name":
                    infografis = infografis.OrderBy(s => s.Judul);
                    break;
                case "Date":
                    infografis = infografis.OrderBy(s => s.Tanggal);
                    break;
                default:
                    infografis = infografis.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 12;
            mymodel.KonsultasiInfografisModel = await PaginatedList<KonsultasiInfografis>.CreateAsync(infografis.AsNoTracking(), page ?? 1, pageSize);
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

            var countItem = (from s in _context.DaftarKonsultasiInfografis
                             where s.Status == published
                             select s).Count();
            var countTrash = (from s in _context.DaftarKonsultasiInfografis
                              where s.Status == trash
                              select s).Count();

            var konsultasiInfografis = from s in _context.DaftarKonsultasiInfografis
                          .Include(ee => ee.Path)
                          .Include(ee => ee.Kategori)
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                konsultasiInfografis = konsultasiInfografis.Where(s => s.Judul.Contains(searchString));
            }

            if (status == null)
            {
                konsultasiInfografis = konsultasiInfografis.Where(s => s.Status == published);
            }
            else if (status == trash)
            {
                konsultasiInfografis = konsultasiInfografis.Where(s => s.Status == trash);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    konsultasiInfografis = konsultasiInfografis.OrderByDescending(s => s.Judul);
                    break;
                case "Name":
                    konsultasiInfografis = konsultasiInfografis.OrderBy(s => s.Judul);
                    break;
                case "Date":
                    konsultasiInfografis = konsultasiInfografis.OrderBy(s => s.Tanggal);
                    break;
                default:
                    konsultasiInfografis = konsultasiInfografis.OrderByDescending(s => s.Tanggal);
                    break;
            }

            int pageSize = 20;
            return View(await PaginatedList<KonsultasiInfografis>.CreateAsync(konsultasiInfografis.AsNoTracking(), page ?? 1, pageSize, countItem, countTrash));
        }

        // GET: Infografis/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infografis = await _context.DaftarKonsultasiInfografis
                .Include(ee => ee.Path)
                .Include(ee => ee.Kategori)
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
        public async Task<IActionResult> Create([Bind("Id,Judul,Tanggal,Path,Source,FImage,Kategori,Tag,Penulis,Status")] KonsultasiInfografis infografis)
        {
            if (ModelState.IsValid)
            {
                infografis.Tanggal = DateTime.Now;
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

            var infografis = await _context.DaftarKonsultasiInfografis.Include(ee => ee.Path).SingleOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Judul,Tanggal,Path,Source,FImage,Kategori,Tag,Penulis,Status")] KonsultasiInfografis infografis)
        {
            if (id != infografis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    infografis.Tanggal = DateTime.Now;
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
                return RedirectToAction(nameof(List));
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

        public async Task<IActionResult> Trash(int id)
        {
            var konsultasiInfografis = await _context.DaftarKonsultasiInfografis.SingleOrDefaultAsync(m => m.Id == id);
            konsultasiInfografis.Status = trash;
            _context.DaftarKonsultasiInfografis.Update(konsultasiInfografis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Restore(int id)
        {
            var konsultasiInfografis = await _context.DaftarKonsultasiInfografis.SingleOrDefaultAsync(m => m.Id == id);
            konsultasiInfografis.Status = published;
            _context.DaftarKonsultasiInfografis.Update(konsultasiInfografis);
            await _context.SaveChangesAsync();
            return RedirectToAction("List", new { status = trash });
        }

        public async Task<IActionResult> RemoveCover(int id)
        {
            var infografis = await _context.DaftarKonsultasiRumahWasathia.SingleOrDefaultAsync(m => m.Id == id);
            infografis.FImage = "";
            _context.DaftarKonsultasiRumahWasathia.Update(infografis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
