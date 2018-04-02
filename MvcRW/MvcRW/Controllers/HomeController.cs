using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcRW.Models;

namespace MvcRW.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Perpustakaan()
        {
            ViewData["Message"] = "Ini adalah halaman perpustakaan, berisi sub menu buku dan artikel.";

            return View();
        }

        public IActionResult Media()
        {
            ViewData["Message"] = "Ini adalah halaman media, berisi sub menu foto dan video.";

            return View();
        }

        public IActionResult Berita()
        {
            ViewData["Message"] = "Ini adalah halaman berita.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
