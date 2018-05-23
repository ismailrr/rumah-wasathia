using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class KajianAudio
    {
        public int Id { get; set; }
        [StringLength(500)]
        public string Judul { get; set; }
        [Required, StringLength(500)]
        public string Link { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }
        public PathKajianAudio Path { get; set; }
        public KategoriKajian Kategori { get; set; }
        public Tag Tag { get; set; }
        public bool Buang { get; set; }
        public bool Post { get; set; }
        public string Penulis { get; set; }

        public string DisplayTextPath
        {
            get
            {
                return $"{Path?.Path}";
            }
        }
    }
}
