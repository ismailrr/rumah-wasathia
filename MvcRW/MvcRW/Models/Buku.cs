using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRW.Models
{
    public class Buku
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Judul { get; set; }
        [Required, StringLength(50)]
        public string Penulis { get; set; }
        [StringLength(50)]
        public string Terbitan { get; set; }
        [StringLength(50)]
        public string ISBN { get; set; }
        [Required, StringLength(500)]
        public string Deskripsi { get; set; }
        public int Tebal { get; set; }
        public DateTime Tanggal { get; set; }
        public PathBuku Path { get; set; }

        public string DisplayTextPath
        {
            get
            {
                return $"{Path?.Path}";
            }
        }
    }
}
