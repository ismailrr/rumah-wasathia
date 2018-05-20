using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class ArtikelKategori
    {
        public int Id { get; set; }
        public int ArtikelId { get; set; }
        public int KategoriId { get; set; }
        //public int KategoriId { get; set; }

        public Artikel Artikel { get; set; }
        public KategoriArtikel Kategori { get; set; }
        //public Kategori Kategori { get; set; }
    }
}
