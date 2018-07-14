using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class KonsultasiEPaper
    {
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Judul { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }
        public PathKonsultasiEPaper Path { get; set; }
        public string Source { get; set; }
        public string FImage { get; set; }
        public KategoriKonsultasi Kategori { get; set; }
        public Tag Tag { get; set; }
        public string Penulis { get; set; }
        public int Status { get; set; }

        public string DisplayTextJudul
        {
            get
            {
                if (Judul != null && Judul.Length > 50)
                {
                    return $"{Judul.Substring(0, 50)}...";
                }
                else
                {
                    return $"{Judul}";
                }
            }
        }
    }
}
