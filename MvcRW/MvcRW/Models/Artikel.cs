using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRW.Models
{
    public class Artikel
    {
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Judul { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }
        public PathArtikel Path { get; set; }
        public KategoriArtikel Kategori { get; set; }

        public string DisplayTextPath
        {
            get
            {
                return $"{Path?.Path}";
            }
        }
    }
}
