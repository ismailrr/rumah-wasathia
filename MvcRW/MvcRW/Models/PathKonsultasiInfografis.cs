using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRW.Models
{
    public class PathKonsultasiInfografis
    {
        public PathKonsultasiInfografis()
        {
            DaftarInfografis = new List<KonsultasiInfografis>();
        }

        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Path { get; set; }
        public DateTime Tanggal { get; set; }

        public ICollection<KonsultasiInfografis> DaftarInfografis { get; set; }
    }
}
