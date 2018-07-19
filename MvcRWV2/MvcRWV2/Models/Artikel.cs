﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class Artikel
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string Judul { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Tanggal { get; set; } //CreatedTime
        public PathArtikel Path { get; set; }
        public string Source { get; set; } //WebContentLink
        public string FImage { get; set; }
        public KategoriArtikel Kategori { get; set; }
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
                if(Judul != null && Judul.Length > 50)
                {
                    return $"{Judul.Substring(0,50)}...";
                }
                else
                {
                    return $"{Judul}";
                }
            }
        }
    }
}
