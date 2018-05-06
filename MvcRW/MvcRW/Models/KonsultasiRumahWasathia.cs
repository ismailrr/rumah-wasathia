using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRW.Models
{
    public class KonsultasiRumahWasathia
    {

        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Judul { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }
        [Required, StringLength(5000)]
        public string Pertanyaan { get; set; }
        [Required, StringLength(5000)]
        public string Jawaban { get; set; }
        [StringLength(200)]
        public string Penulis { get; set; }
        public KategoriKonsultasi Kategori { get; set; }
    }
}
