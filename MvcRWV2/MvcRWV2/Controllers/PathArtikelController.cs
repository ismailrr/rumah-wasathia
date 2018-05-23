using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using MvcRWV2.Data;
using MvcRWV2.Models;
using MvcRWV2.ViewModels;

namespace MvcRWV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class PathArtikelController : Controller
    {
        protected Repository Repository { get; private set; }
        private PathArtikelRepository _pathArtikelRepository = null;
        private readonly IHostingEnvironment _environment;

        public PathArtikelController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _pathArtikelRepository = new PathArtikelRepository(context);
            _environment = environment;
        }

        // GET: PathArtikel
        [AllowAnonymous]
        public IActionResult Index()
        {

            var pathArtikel = _pathArtikelRepository.GetList();

            return View(pathArtikel);
        }

        // GET: PathArtikel/Details/5
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pathArtikel = _pathArtikelRepository.Get((int)id);

            if (pathArtikel == null)
            {
                return NotFound();
            }

            return View(pathArtikel);
        }

        // GET: PathArtikel/Create
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new PathArtikelAddVM();

            viewModel.Init(Repository);

            return View(viewModel);
        }

        // POST: PathArtikel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] PathArtikelAddVM pathArtikelVM)
        {
            if (pathArtikelVM !=null && ModelState.IsValid)
            {
                //Directory.GetCurrentDirectory(),
                var filePath = Path.Combine("Uploads", pathArtikelVM.Path.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    pathArtikelVM.Path.CopyToAsync(stream);
                }

                PathArtikel pathArtikel = new PathArtikel() 
                {
                    Path = pathArtikelVM.Path.FileName,
                    Tanggal = DateTime.Now
                };

                _pathArtikelRepository.Add(pathArtikel);

                //Message = $"Upload document {UploadFile.FilePath} has been successfully!";

                return RedirectToAction(nameof(Index));
            }

            pathArtikelVM.Init(Repository);

            return View(pathArtikelVM);
        }

        // GET: PathArtikel/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pathArtikel = _pathArtikelRepository.Get((int)id,
                includeRelatedEntities: false);

            if (pathArtikel == null)
            {
                return NotFound();
            }

            var viewModel = new PathArtikelEditVM()
            {
                PathArtikel = pathArtikel
            };
            viewModel.Init(Repository);

            return View(pathArtikel);
        }

        // POST: PathArtikel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PathArtikelEditVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var pathArtikel = viewModel.PathArtikel;
                _pathArtikelRepository.Update(pathArtikel);
                
                return RedirectToAction("Detail", new { id = pathArtikel.Id });
            }
            viewModel.Init(Repository);
            return View(viewModel);
        }

        // GET: PathArtikel/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pathArtikel = _pathArtikelRepository.Get((int)id);
            if (pathArtikel == null)
            {
                return NotFound();
            }

            return View(pathArtikel);
        }

        // POST: PathArtikel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _pathArtikelRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        //cek data sama
        //private bool PathArtikelExists(int id)
        //{
        //    return _context.DaftarPathArtikel.Any(e => e.Id == id);
        //}
    }
}
