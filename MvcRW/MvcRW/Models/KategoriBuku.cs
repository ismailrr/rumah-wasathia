using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRW.Models
{
    public class KategoriBuku
    {
        public KategoriBuku()
        {
            DaftarBuku = new List<Buku>();
        }

        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Nama { get; set; }
        public DateTime Tanggal { get; set; }

        public ICollection<Buku> DaftarBuku { get; set; }
    }
}
