using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcRWV2.Data;
using MvcRWV2.Models;

namespace MvcRWV2.Controllers
{
    public class rumahwasathiaController : Controller
    {
        private readonly ApplicationDbContext _context;
            
        public rumahwasathiaController(ApplicationDbContext context)
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
