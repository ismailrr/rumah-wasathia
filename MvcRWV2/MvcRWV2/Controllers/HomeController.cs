using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcRWV2.Models;

namespace MvcRWV2.Controllers
{
    
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        GoogleDriveFilesRepository driveService;
        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            driveService = new GoogleDriveFilesRepository(_hostingEnvironment);
        }

        [HttpGet]
        public ActionResult GetGoogleDriveFiles()
        {
            return View(driveService.GetDriveFilesArtikel());
        }

        [HttpPost]
        public ActionResult DeleteFile(string id)
        {
            driveService.DeleteFile(id);
            return RedirectToAction("GetGoogleDriveFiles");
        }

        [HttpPost]
        public ActionResult MoveFileToArtikelFolder(GoogleDriveFiles file, string parentId)
        {
            driveService.MoveFileTo(file, parentId);
            return RedirectToAction("GetGoogleDriveFiles");
        }

        [HttpPost]
        public ActionResult UploadFile(IFormFile file)
        {
            driveService.FileUpload(file);
            return RedirectToAction("GetGoogleDriveFiles");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileArtikel(IFormFile file)
        {
            try
            {
                DriveService service = driveService.GetService();
            var folderId = "1MeEImyGO6ma6mn9m-UiiNDNb9OX_F63S";
            string path = Path.GetTempFileName();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(file.FileName),
                Parents = new List<string>
                {
                    folderId
                }
            };
            FilesResource.CreateMediaUpload request;
            if (file.Length > 0)
            {
                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                {
                    await file.CopyToAsync(stream);
                    request = service.Files.Create(
                        fileMetadata, stream, "application/pdf");
                    request.Fields = "id";
                    request.Upload();
                }
            }
            }
            catch
            {

            }
            
            return RedirectToAction("GetGoogleDriveFiles");
        }

        public void DownloadFile(string id)
        {
            string FilePath = driveService.DownloadGoogleFile(id);


            Response.ContentType = "application/zip";
            Response.WriteAsync(_hostingEnvironment.ContentRootPath);
        }

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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
