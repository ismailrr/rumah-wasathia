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
        [Required, StringLength(100)]
        public string Judul { get; set; }
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

        //public ICollection<ArtikelKategori> DaftarKategori { get; set; }

        //public void TambahKategori(KategoriArtikel kategori)
        //{
        //    DaftarKategori.Add(new ArtikelKategori()
        //    {
        //        Kategori = kategori
        //    });
        //}

        //public void TambahKategori(int kategoriId)
        //{
        //    DaftarKategori.Add(new ArtikelKategori()
        //    {
        //        KategoriId = kategoriId
        //    });
        //}
    }
}
