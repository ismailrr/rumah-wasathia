﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRW.Models
{
    public class KonsultasiInfografis
    {
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Judul { get; set; }
        public DateTime Tanggal { get; set; }
        public PathKonsultasiInfografis Path { get; set; }
        public KategoriKonsultasi Kategori { get; set; }

        public string DisplayTextPath
        {
            get
            {
                return $"{Path?.Path}";
            }
        }
    }
}
