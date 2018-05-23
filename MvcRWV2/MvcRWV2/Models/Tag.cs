using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class Tag
    {
        public Tag()
        {
        }

        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Nama { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }
    }
}
