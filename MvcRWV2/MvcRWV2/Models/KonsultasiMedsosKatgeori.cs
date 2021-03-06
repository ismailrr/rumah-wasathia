﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class KonsultasiMedsos
    {

        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Judul { get; set; }
        [Required]
        public DateTime Tanggal { get; set; }
        public string Pertanyaan { get; set; }
        public string Jawaban { get; set; }

        public Kategori Kategori { get; set; }
    }
}
