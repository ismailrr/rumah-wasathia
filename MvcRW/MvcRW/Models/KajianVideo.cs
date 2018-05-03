using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRW.Models
{
    public class KajianVideo
    {
        public int Id { get; set; }
        [Required, StringLength(500)]
        public string Link { get; set; }
        public DateTime Tanggal { get; set; }
        public PathKajianVideo Path { get; set; }
        public KategoriKajian Kategori { get; set; }

        public string DisplayTextPath
        {
            get
            {
                return $"{Path?.Path}";
            }
        }
    }
}
