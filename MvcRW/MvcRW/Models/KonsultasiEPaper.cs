using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRW.Models
{
    public class KonsultasiEPaper
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Judul { get; set; }
        [Required]
        public DateTime Tanggal { get; set; }
        [Required]
        public PathKonsultasiEPaper Path { get; set; }
        public KategoriKonsultasi Kategori { get; set; }

    }
}
