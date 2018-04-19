﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcRW.Models;

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
        public ActionResult galeri1()
        {
            return View();
        }
        // GET: /rumahwasathia/Sub-Galeri/
        public ActionResult galeri2()
        {
            return View();
        }
        // GET: /rumahwasathia/SubBuku/
        public ActionResult SubBuku()
        {
            return View();
        }
        // GET: /rumahwasathia/ChannelMuamalah/
        public ActionResult ChannelMuamalah()
        {
            return View();
        }
        // GET: /rumahwasathia/ChannelRumahWasathia/
        public ActionResult ChannelRumahWasathia()
        {
            return View();
        }
        // GET: /rumahwasathia/SubArtikel/
        public ActionResult SubArtikel()
        {
            return View();
        }
        // GET: /rumahwasathia/Konsultasi Republika/
        public ActionResult KonsultasiRepublika()
        {
            return View();
        }
        // GET: /rumahwasathia/Rebuplika Online/
        public ActionResult republikaOnline()
        {
            return View();
        }
        // GET: /rumahwasathia/ViaMedsos/
        public ActionResult ViaMediaSosial()
        {
            return View();
        }
        // Admin
        public ActionResult Berita()
        {
            return View();
        }

        public ActionResult AGaleri()
        {
            return View();
        }

        public ActionResult Aartikel()
        {
            return View();
        }

        public ActionResult AKonsultasi()
        {
            return View();
        }

        public ActionResult APdf()
        {
            return View();
        }

        public ActionResult ABuku()
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

        public ActionResult AIndex()
        {
            return View();
        }

        public ActionResult AAudio()
        {
            return View();
        }

        public ActionResult AVideo()
        {
            return View();
        }
    }
}
