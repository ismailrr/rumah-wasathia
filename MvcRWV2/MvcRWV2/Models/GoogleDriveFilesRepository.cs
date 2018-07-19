using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MvcRWV2.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;

namespace MvcRWV2.Models
{
    public class GoogleDriveFilesRepository 
    {
        private readonly ApplicationDbContext _context;
        private int published = 1;
        private int trash = 2;
        public static string[] Scopes = { DriveService.Scope.Drive };
        private readonly IHostingEnvironment _environment;

        public GoogleDriveFilesRepository (IHostingEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public DriveService GetService()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            //Create Drive API service.
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MvcRWV2",
            });

            return service;
        }

        public List<GoogleDriveFiles> GetDriveFiles()
        {
            DriveService service = GetService();
            
            // Define parameters of request.
            FilesResource.ListRequest FileListRequest = service.Files.List();

            //listRequest.PageSize = 10;
            //listRequest.PageToken = 10;
            FileListRequest.Fields = "nextPageToken, files(id, name, size, version, trashed, createdTime,contentHints/thumbnail,thumbnailLink,fileExtension,iconLink,thumbnailLink,webContentLink,webViewLink,mimeType,parents)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
            List<GoogleDriveFiles> FileList = new List<GoogleDriveFiles>();

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    GoogleDriveFiles File = new GoogleDriveFiles {
                        Id = file.Id,
                        Name = file.Name,
                        Size = file.Size,
                        Version = file.Version,
                        CreatedTime = file.CreatedTime,
                        Type = file.FileExtension,
                        Thumbnail = file.HasThumbnail,
                        ThumbnailLink = file.ThumbnailLink,
                        IconLink = file.IconLink,
                        WebContentLink = file.WebContentLink ,
                        WebViewLink = file.WebViewLink,
                        Parents = file.Parents,
                        MimeType = file.MimeType
                    };
                    FileList.Add(File);
                }
            }
            return FileList;
        }

        public List<GoogleDriveFiles> GetDriveFilesArtikel()
        {
            DriveService service = GetService();

            // Define parameters of request.
            FilesResource.ListRequest FileListRequest = service.Files.List();

            //listRequest.PageSize = 10;
            //listRequest.PageToken = 10;
            FileListRequest.PageSize = 1000;
            FileListRequest.Fields = "nextPageToken, files(id, name, size, version, trashed, createdTime,contentHints/thumbnail,thumbnailLink,fileExtension,iconLink,thumbnailLink,webContentLink,webViewLink,mimeType,parents)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
            List<GoogleDriveFiles> FileList = new List<GoogleDriveFiles>();

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.MimeType.Equals("application/pdf") && file.Parents.Contains("1MeEImyGO6ma6mn9m-UiiNDNb9OX_F63S"))
                    {
                        GoogleDriveFiles File = new GoogleDriveFiles
                        {
                            Id = file.Id,
                            Name = file.Name,
                            Size = file.Size,
                            Version = file.Version,
                            CreatedTime = file.CreatedTime,
                            Type = file.FileExtension,
                            Thumbnail = file.HasThumbnail,
                            ThumbnailLink = file.ThumbnailLink,
                            IconLink = file.IconLink,
                            WebContentLink = file.WebContentLink,
                            WebViewLink = file.WebViewLink,
                            Parents = file.Parents,
                            MimeType = file.MimeType
                        };
                        FileList.Add(File);
                    }
                }
            }
            return FileList;
        }

        public void MoveFileTo(GoogleDriveFiles files, string parentId)
        {
            DriveService service = GetService();

            var fileId = files.Id;
            var folderId = parentId;
            // Retrieve the existing parents to remove
            var getRequest = service.Files.Get(fileId);
            getRequest.Fields = "parents";
            var file = getRequest.Execute();
            var previousParents = String.Join(",", file.Parents);
            // Move the file to the new folder
            var updateRequest = service.Files.Update(new Google.Apis.Drive.v3.Data.File(), fileId);
            updateRequest.Fields = "id, parents";
            updateRequest.AddParents = folderId;
            updateRequest.RemoveParents = previousParents;
            file = updateRequest.Execute();
        }

        public void FileUpload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                DriveService service = GetService();

                string path = Path.GetTempFileName() ;

                var FileMetaData = new Google.Apis.Drive.v3.Data.File();
                FileMetaData.Name = Path.GetFileName(file.FileName);

                FilesResource.CreateMediaUpload request;

                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }
            }
        }

        public async void FileUploadTo(IFormFile file, string parentId)
        {
            DriveService service = GetService();
            var folderId = parentId;
            string path = Path.Combine(_environment.WebRootPath, "uploads\\pdf\\artikel");
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
                    request = service.Files.Create(
                        fileMetadata, stream, "application/pdf");
                    request.Fields = "id";
                    request.Upload();
                    await file.CopyToAsync(stream);
                }
            }
                
            //var fileUploaded = request.ResponseBody;

        }

        public string DownloadGoogleFile(string fileId)
        {
            DriveService service = GetService();

            string FolderPath = Path.GetTempFileName();
            FilesResource.GetRequest request = service.Files.Get(fileId);

            string FileName = request.Execute().Name;
            string FilePath = System.IO.Path.Combine(FolderPath, FileName);

            MemoryStream stream1 = new MemoryStream();

            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case DownloadStatus.Downloading:
                        {
                            //Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                    case DownloadStatus.Completed:
                        {
                            //Console.WriteLine("Download complete.");
                            SaveStream(stream1, FilePath);
                            break;
                        }
                    case DownloadStatus.Failed:
                        {
                            //Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };
            request.Download(stream1);
            return FilePath;
        }

        private static void SaveStream(MemoryStream stream, string FilePath)
        {
            using (System.IO.FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }
        }

        public void DeleteFile(string id)
        {
            DriveService service = GetService();
            try
            {
                // Initial validation.
                if (service == null)
                    throw new ArgumentNullException("service");

                // Make the request.
                service.Files.Delete(id).Execute();
            }
            catch (Exception ex)
            {
                throw new Exception("Request Files.Delete failed.", ex);
            }
        }
    }
}