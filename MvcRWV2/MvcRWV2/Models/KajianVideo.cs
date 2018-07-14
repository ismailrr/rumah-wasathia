using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcRWV2.Models
{
    public class KajianVideo
    {
        public int Id { get; set; }
        [StringLength(500)]
        public string Judul { get; set; }
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }
        public PathKajianVideo Path { get; set; }
        [Required, StringLength(500)]
        public string Source { get; set; }
        public string FImage { get; set; }
        public KategoriKajian Kategori { get; set; }
        public Tag Tag { get; set; }
        public string Penulis { get; set; }
        public int Status { get; set; }

        public string DisplayTextPath
        {
            get
            {
                return $"{Path?.Path}";
            }
        }

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

        public string DisplayTanggalFormat1
        {
            get
            {
                if (Tanggal != null)
                {
                    return $"{Tanggal.Day} {DisplayTextBulan} {Tanggal.Year} , {Tanggal.Hour}:{Tanggal.Minute}";
                }
                else
                {
                    return $"{Tanggal}";
                }
            }
        }

        public string DisplayTextBulan
        {
            get
            {
                if (Tanggal != null)
                {
                    return $"{Tanggal.ToString("MMMMMMMM", CultureInfo.InvariantCulture)}";
                }
                else
                {
                    return $"{Tanggal}";
                }
            }
        }

        public string DisplayTextHari
        {
            get
            {
                if (Tanggal != null)
                {
                    return $"{Tanggal.ToString("dddddddd", CultureInfo.InvariantCulture)}";
                }
                else
                {
                    return $"{Tanggal}";
                }
            }
        }
    }
}
