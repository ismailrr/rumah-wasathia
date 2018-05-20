using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class KonsultasiRepublika
    {

        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Judul { get; set; }
        [Required, StringLength(500)]
        public string Link { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }
        public KategoriKonsultasi Kategori { get; set; }
    }
}
