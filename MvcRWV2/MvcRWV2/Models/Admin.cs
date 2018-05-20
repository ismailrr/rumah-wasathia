using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class Admin
    {
        public int Id { get; set; }
        [Required, StringLength(50), Display(Name = "Nama Pengguna")]
        public string NamaPengguna { get; set; }
        [Required, StringLength(50), Display(Name = "Kata Sandi"), DataType(DataType.Password)]
        public string KataSandi { get; set; }
        public bool RememberMe { get; set; }
        public DateTime Tanggal { get; set; }

    }
}
