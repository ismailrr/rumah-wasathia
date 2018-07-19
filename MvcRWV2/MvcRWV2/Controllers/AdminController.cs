using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcRWV2.Data;
using MvcRWV2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MvcRWV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Artikel()
        {
            return View();
        }

        public IActionResult Audio()
        {
            return View();
        }

        public IActionResult Buku()
        {
            return View();
        }

        public IActionResult Galeri()
        {
            return View();
        }

        public IActionResult Kategori()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult KonsultasiEpaper()
        {
            return View();
        }

        public IActionResult KonsultasiInfografis()
        {
            return View();
        }

        public IActionResult KonsultasiRepublika()
        {
            return View();
        }

        public IActionResult KonsultasiRumahWasathia()
        {
            return View();
        }

        public IActionResult Video()
        {
            return View();
        }
    }
}