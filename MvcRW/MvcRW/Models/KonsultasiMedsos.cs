using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRW.Models
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
        [Required]
        public KategoriKonsultasi Kategori { get; set; }
    }
}
