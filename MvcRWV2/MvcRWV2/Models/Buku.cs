using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class Buku
    {
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Judul { get; set; }
        [StringLength(200)]
        public string PenulisBuku { get; set; }
        [StringLength(50), Display(Name = "Tahun Terbit")]
        public string Terbitan { get; set; }
        [StringLength(50)]
        public string ISBN { get; set; }
        [StringLength(5000)]
        public string Deskripsi { get; set; }
        public int Tebal { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }
        public string Source { get; set; }
        public PathBuku Path { get; set; }
        public string FImage { get; set; }
        public KategoriBuku Kategori { get; set; }
        public Tag Tag { get; set; }
        public string Penulis { get; set; }
        public int Status { get; set; }
        public string DriveId { get; set; } // Id
        public long? Size { get; set; }
        public long? Version { get; set; }
        public string Type { get; set; } //MIME Type
        public bool? Thumbnail { get; set; }
        public string ThumbnailLink { get; set; }
        public string IconLink { get; set; }
        public string WebViewLink { get; set; }
        public string Parents { get; set; }

        public string DisplayTextPath
        {
            get
            {
                return $"{Path?.Path}";
            }
        }

        public string DisplayTextJudul
        {
            get
            {
                if (Judul != null && Judul.Length > 50)
                {
                    return $"{Judul.Substring(0, 50)}...";
                }
                else
                {
                    return $"{Judul}";
                }
            }
        }
        public string DisplayTanggalFormat1
        {
            get
            {
                if (Tanggal != null)
                {
                    return $"{Tanggal.Day} {DisplayTextBulan} {Tanggal.Year} , {Tanggal.Hour}:{Tanggal.Minute}";
                }
                else
                {
                    return $"{Tanggal}";
                }
            }
        }

        public string DisplayTextBulan
        {
            get
            {
                if (Tanggal != null)
                {
                    return $"{Tanggal.ToString("MMMMMMMM", CultureInfo.InvariantCulture)}";
                }
                else
                {
                    return $"{Tanggal}";
                }
            }
        }

        public string DisplayTextHari
        {
            get
            {
                if (Tanggal != null)
                {
                    return $"{Tanggal.ToString("dddddddd", CultureInfo.InvariantCulture)}";
                }
                else
                {
                    return $"{Tanggal}";
                }
            }
        }
    }
}
