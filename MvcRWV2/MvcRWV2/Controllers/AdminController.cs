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

        [BindProperty] // Bind on Post
        public Admin LoginData { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var admin = from s in _context.DaftarAdmin
                          select s;

            if (ModelState.IsValid)
            {
                var isValid = (LoginData.NamaPengguna == "username" && LoginData.KataSandi == "password"); // TODO Validate the username and the password with your own logic
                if (!isValid)
                {
                    ModelState.AddModelError("", "username or password is invalid");
                    return RedirectToPage("Login");
                }
                // Create the identity from the user info
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, LoginData.NamaPengguna));
                identity.AddClaim(new Claim(ClaimTypes.Name, LoginData.NamaPengguna));
                // Authenticate using the identity
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = LoginData.RememberMe });
                return RedirectToPage("Index");
            }
            else
            {
                ModelState.AddModelError("", "username or password is blank");
                return RedirectToPage("Login");
            }
        }
    }
}