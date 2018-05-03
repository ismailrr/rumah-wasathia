using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRW.Models
{
    public class Galeri
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Judul { get; set; }
        [Required]
        public DateTime Tanggal { get; set; }
        [Required]    
        public PathGaleri Path { get; set; }
        public KategoriGaleri Kategori { get; set; }

    }
}
