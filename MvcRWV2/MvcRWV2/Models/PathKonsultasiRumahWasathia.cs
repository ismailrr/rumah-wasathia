﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class PathKonsultasiRumahWasathia
    {
        public PathKonsultasiRumahWasathia()
        {
            DaftarkonsultasiRumahWasathia = new List<KonsultasiRumahWasathia>();
        }

        public int Id { get; set; }
        [Required, StringLength(500)]
        public string Path { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }

        public ICollection<KonsultasiRumahWasathia> DaftarkonsultasiRumahWasathia { get; set; }
    }
}