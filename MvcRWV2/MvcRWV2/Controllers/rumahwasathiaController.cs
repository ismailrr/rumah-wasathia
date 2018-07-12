using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcRWV2.Data;
using MvcRWV2.Models;
using MvcRWV2.Models.rumahwasathiaViewModels;

namespace MvcRWV2.Controllers
{
    public class rumahwasathiaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int published = 1;
        private int trash = 2;

        public rumahwasathiaController(ApplicationDbContext context)
        {
            _context = context;
        }

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
            IndexViewModel mymodel = new IndexViewModel();

            mymodel.ArtikelModel = from s in _context.DaftarArtikel
                       .Include(ee => ee.Kategori)
                       .Take(12)
                                   where s.Status == published
                                   orderby (s.Tanggal)
                                   select s;
            mymodel.BukuModel = from s in _context.DaftarBuku
                       .Include(ee => ee.Kategori)
                       .Include(ee => ee.Path)
                       .Take(4)
                                where s.Status == published
                                orderby (s.Tanggal)
                                select s;
            mymodel.GaleriModel = from s in _context.DaftarGaleri
                       .Include(ee => ee.Kategori)
                       .Take(6)
                                  orderby (s.Tanggal)
                                  select s;
            mymodel.KajianAudioModel = from s in _context.DaftarKajianAudio
                       .Include(ee => ee.Kategori)
                       .Take(4)
                                       where s.Status == published
                                       orderby (s.Tanggal)
                                       select s;
            mymodel.KajianVideoModel = from s in _context.DaftarKajianVideo
                       .Include(ee => ee.Kategori)
                       .Take(4)
                                       where s.Status == published
                                       orderby (s.Tanggal)
                                       select s;

            var artikel = mymodel.ArtikelModel;
            var buku = mymodel.BukuModel;
            var galeri = mymodel.GaleriModel;
            var kajianAudio = mymodel.KajianAudioModel;
            var kajianVideo = mymodel.KajianVideoModel;

            return View(mymodel);
        }

        public ActionResult Beranda()
        {
            ViewData["Message"] = "This is the welcome action method...";

            return View();
        }
        
        public ActionResult profil()
        {
            return View();
        }
        
        public ActionResult biografi()
        {
            return View();
        }
        
        public ActionResult ChannelMuamalah()
        {
            return View();
        }
        
        public ActionResult ChannelRumahWasathia()
        {
            return View();
        }

        public ActionResult rumahwasathia()
        {
            return View();
        }
        // Admin
        public ActionResult Berita()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
