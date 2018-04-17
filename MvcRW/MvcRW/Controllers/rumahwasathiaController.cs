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
        public ActionResult profil()
        {
            return View();
            //return "This is the profil action method...";
        }

        // GET: /rumahwasathia/Biografi/
        public ActionResult biografi()
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
        // GET: /rumahwasathia/Galeri/
        public ActionResult galeri()
        {
            return View();
        }
        // GET: /rumahwasathia/Sub-Galeri/
        public ActionResult SubGaleri()
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
        // GET: /rumahwasathia/tes(It's preview requsted from client)/
        public ActionResult tes()
        {
            return View();
        }
        // GET: /rumahwasathia/SubArtikel/
        public ActionResult SubArtikel()
        {
            return View();
        }
        // GET: /rumahwasathia/republika/
        public ActionResult republika()
        {
            return View();
        }
        // GET: /rumahwasathia/mingguan/
        public ActionResult mingguan()
        {
            return View();
        }

        // Admin
        public ActionResult ABerita()
        {
            return View();
        }
        public ActionResult AArtikel()
        {
            return View();
        }
        public ActionResult APdf()
        {
            return View();
        }
        public ActionResult AKategori()
        {
            return View();
        }
        public ActionResult ATag()
        {
            return View();
        }
        public ActionResult ATanyaJawab()
        {
            return View();
        }
        public ActionResult AIndex()
        {
            return View();
        }
    }
}
