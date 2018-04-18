using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRW.Models
{
    public class KategoriKonsultasi
    {
        public KategoriKonsultasi()
        {
            DaftarKonsultasiMedsos = new List<KonsultasiMedsos>();
            DaftarKonsultasiRepublika = new List<KonsultasiRepublika>();
        }

        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Nama { get; set; }

        public ICollection<KonsultasiMedsos> DaftarKonsultasiMedsos { get; set; }
        public ICollection<KonsultasiRepublika> DaftarKonsultasiRepublika { get; set; }
    }
}
