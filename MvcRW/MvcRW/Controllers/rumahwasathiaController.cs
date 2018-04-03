﻿using System;
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
        public ActionResult Biografi()
        {
            return View();
        }

        // GET: /rumahwasathia/TanyaJawab/
        public ActionResult tanya_jawab()
        {
            return View();
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
        public ActionResult Audio()
        {
            return View();
        }
        // GET: /rumahwasathia/KajianSuara/
        public ActionResult Video()
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
