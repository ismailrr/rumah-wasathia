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
            DaftarKonsultasiEPaper = new List<KonsultasiEPaper>();
            DaftarKonsultasiInfografis = new List<KonsultasiInfografis>();
            DaftarKonsultasiRumahWasathia = new List<KonsultasiRumahWasathia>();
            DaftarKonsultasiRepublika = new List<KonsultasiRepublika>();
        }

        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Nama { get; set; }
        public DateTime Tanggal { get; set; }

        public ICollection<KonsultasiEPaper> DaftarKonsultasiEPaper { get; set; }
        public ICollection<KonsultasiInfografis> DaftarKonsultasiInfografis { get; set; }
        public ICollection<KonsultasiRumahWasathia> DaftarKonsultasiRumahWasathia { get; set; }
        public ICollection<KonsultasiRepublika> DaftarKonsultasiRepublika { get; set; }
    }
}
