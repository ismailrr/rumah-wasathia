using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcRW.Data;
using MvcRW.Models;

namespace MvcRW.Controllers
{
    public class rumahwasathiaController : Controller
    {
        private readonly RWContext _context;
            
        public rumahwasathiaController(RWContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Buku()
        {
            return View();
        }
        
        public ActionResult Audio()
        {
            return View();
        }
        
        public ActionResult Video()
        {
            return View();
        }
        
        public ActionResult SubBuku1()
        {
            return View();
        }
        
        public ActionResult SubBuku2()
        {
            return View();
        }
        
        public ActionResult SubBuku3()
        {
            return View();
        }
        
        public ActionResult SubBuku4()
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
        
        public ActionResult SubArtikel()
        {
            return View();
        }

        public ActionResult republikaOnline()
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

        public ActionResult ALogin()
        {
            return View();
        }
    }
}
