﻿using System;
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
        [Required]
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }
        [Required]    
        public PathGaleri Path { get; set; }
        public KategoriGaleri Kategori { get; set; }

    }
}