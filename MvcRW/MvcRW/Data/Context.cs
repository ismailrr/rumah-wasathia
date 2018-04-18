using Microsoft.EntityFrameworkCore;
using MvcRW.Models;
using System;

namespace MvcRW.Data
{
    public class Context : DbContext
    {
        /// <summary>
        /// Entity Framework context class.
        /// </summary>
        public DbSet<Artikel> DaftarArtikel { get; set; }
        public DbSet<Galeri> DaftarGaleri { get; set; }
        public DbSet<KategoriArtikel> DaftarKategoriArtikel { get; set; }
        public DbSet<KategoriKonsultasi> DaftarKategoriKonsultasi { get; set; }
        public DbSet<KonsultasiMedsos> DaftarKonsultasiMedsos { get; set; }
        public DbSet<KonsultasiRepublika> DaftarKonsulatasiRepublika { get; set; }
        public DbSet<PathArtikel> DaftarPathArtikel { get; set; }
        public DbSet<PathGaleri> DaftarPathGaleri { get; set; }
        public DbSet<PathKonsultasiRepublika> PathKonsultasiRepublika { get; set; }



        public override void Dispose()
        {
            throw new NotImplementedException();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
