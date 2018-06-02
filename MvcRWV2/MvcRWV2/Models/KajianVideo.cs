﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class KajianVideo
    {
        public int Id { get; set; }
        [StringLength(500)]
        public string Judul { get; set; }
        [Required, StringLength(500)]
        public string Link { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }
        public PathKajianVideo Path { get; set; }
        public KategoriKajian Kategori { get; set; }
        public Tag Tag { get; set; }
        public string Penulis { get; set; }
        public int Status { get; set; }

        public string DisplayTextPath
        {
            get
            {
                return $"{Path?.Path}";
            }
        }
    }
}
