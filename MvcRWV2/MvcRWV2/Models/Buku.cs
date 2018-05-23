﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public PathBuku Path { get; set; }
        public KategoriBuku Kategori { get; set; }
        public Tag Tag { get; set; }
        public bool Buang { get; set; }
        public bool Post { get; set; }
        public string Penulis { get; set; }

        public string DisplayTextPath
        {
            get
            {
                return $"{Path?.Path}";
            }
        }
    }
}