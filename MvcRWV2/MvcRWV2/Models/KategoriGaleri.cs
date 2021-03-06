﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class KategoriGaleri
    {
        public KategoriGaleri()
        {
            DaftarGaleri = new List<Galeri>();
        }

        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Nama { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }

        public ICollection<Galeri> DaftarGaleri { get; set; }
    }
}
