using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class Galeri
    {
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Judul { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; } 
        public PathGaleri Path { get; set; }
        public string Source { get; set; }
        public string FImage { get; set; }
        public KategoriGaleri Kategori { get; set; }
        public Tag Tag { get; set; }
        public int Status { get; set; }
        public int Jumlah { get; set; }
        public string Penulis { get; set; }
        public string DriveId { get; set; } // Id
        public long? Size { get; set; }
        public long? Version { get; set; }
        public string Type { get; set; } //MIME Type
        public bool? Thumbnail { get; set; }
        public string ThumbnailLink { get; set; }
        public string IconLink { get; set; }
        public string WebViewLink { get; set; }
        public string Parents { get; set; }
    }
}
