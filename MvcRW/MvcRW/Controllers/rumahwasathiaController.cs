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

        // GET: /rumahwasathia/Perpustakaan/
        public string Perpustakaan()
        {
            return "This is the perpustakaan action method...";
        }

        // GET: /rumahwasathia/Media/
        public string Media()
        {
            return "This is the media action method...";
        }

        // GET: /rumahwasathia/Berita/
        public string Berita()
        {
            return "This is the berita action method...";
        }
    }
}
