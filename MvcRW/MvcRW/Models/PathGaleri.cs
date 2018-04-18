﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRW.Models
{
    public class PathGaleri
    {
        public PathGaleri()
        {
            DaftarGaleri = new List<Galeri>();
        }

        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Path { get; set; }

        public ICollection<Galeri> DaftarGaleri { get; set; }
    }
}
