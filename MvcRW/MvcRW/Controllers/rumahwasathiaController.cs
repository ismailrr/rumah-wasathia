using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcRW.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcRW.Controllers
{
    public class rumahwasathiaController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /rumahwasathia/Beranda/
        public ActionResult Beranda()
        {
            ViewData["Message"] = "This is the welcome action method...";

            return View();
            //return "This is the welcome action method...";
        }

        // GET: /rumahwasathia/Profil/
        public ActionResult Profil()
        {
            return View();
            //return "This is the profil action method...";
        }

        // GET: /rumahwasathia/Biografi/
        public string Biografi()
        {
            return "This is the biografi action method...";
        }

        // GET: /rumahwasathia/TanyaJawab/
        public string TanyaJawab()
        {
            return "This is the TanyaJawab action method...";
        }

        // GET: /rumahwasathia/Artikel/
        public ActionResult Artikel()
        {
            return View();
        }

        // GET: /rumahwasathia/Buku/
        public ActionResult Buku()
        {
            return View();
        }
        // GET: /rumahwasathia/KajianVideo/
        public ActionResult audio()
        {
            return View();
        }
        // GET: /rumahwasathia/KajianSuara/
        public ActionResult video()
        {
            return View();
        }
        // GET: /rumahwasathia/Berita/
        public ActionResult Berita()
        {
            return View();
        }
        // GET: /rumahwasathia/Galeri/
        public ActionResult Galeri()
        {
            return View();
        }
        // GET: /rumahwasathia/SubBuku(Fiqih)/
        public ActionResult SubBuku()
        {
            return View();
        }
        // GET: /rumahwasathia/SubBuku(Fiqih)/
        public ActionResult fiqih()
        {
            return View();
        }
    }
}
