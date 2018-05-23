﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class KonsultasiRumahWasathia
    {

        public int Id { get; set; }
        [StringLength(200)]
        public string PenulisKonten { get; set; }
        [Required, StringLength(200)]
        public string Judul { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }
        [Required, StringLength(5000)]
        public string Pertanyaan { get; set; }
        [Required, StringLength(5000)]
        public string Jawaban { get; set; }
        public PathKonsultasiRumahWasathia Path { get; set; }
        public KategoriKonsultasi Kategori { get; set; }
        public Tag Tag { get; set; }
        public bool Buang { get; set; }
        public bool Post { get; set; }
        public string Penulis { get; set; }
    }
}
