using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcRWV2.Models
{
    public class GoogleDriveFiles
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long? Size { get; set; }
        public long? Version { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string Type { get; set; }
        public bool? Thumbnail { get; set; }
        public string ThumbnailLink { get; set; }
        public string IconLink { get; set; }
        public string WebContentLink { get; set; }
        public string WebViewLink { get; set; }
        public string MimeType { get; set; }
        public IList<string> Parents { get; set; }
        
    }
}