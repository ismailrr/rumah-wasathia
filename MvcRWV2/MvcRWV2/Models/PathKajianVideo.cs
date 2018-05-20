using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class PathKajianVideo
    {
        public PathKajianVideo()
        {
            DaftarKajianVideo = new List<KajianVideo>();
        }

        public int Id { get; set; }
        [Required, StringLength(500)]
        public string Path { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }

        public ICollection<KajianVideo> DaftarKajianVideo { get; set; }
    }
}
