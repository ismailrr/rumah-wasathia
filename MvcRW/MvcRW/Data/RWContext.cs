using Microsoft.EntityFrameworkCore;
using MvcRW.Models;
using MySql.Data.MySqlClient;
using System;

namespace MvcRW.Data
{
    public class RWContext : DbContext
    {
        public RWContext(DbContextOptions<RWContext> options)
            : base(options)
        { }

        /// <summary>
        /// Entity Framework context class.
        /// </summary>
        public DbSet<Artikel> DaftarArtikel { get; set; }
        public DbSet<Galeri> DaftarGaleri { get; set; }
        public DbSet<Infografis> DaftarInfografis { get; set; }
        public DbSet<KategoriArtikel> DaftarKategoriArtikel { get; set; }
        public DbSet<KategoriKonsultasi> DaftarKategoriKonsultasi { get; set; }
        public DbSet<KonsultasiRumahWasathia> DaftarKonsultasiRumahWasathia { get; set; }
        public DbSet<KonsultasiEPaper> DaftarKonsultasiEPaper { get; set; }
        public DbSet<KonsultasiRepublika> DaftarKonsultasiRepublika { get; set; }
        public DbSet<PathArtikel> DaftarPathArtikel { get; set; }
        public DbSet<PathGaleri> DaftarPathGaleri { get; set; }
        public DbSet<PathInfografis> DaftarPathInfografis { get; set; }
        public DbSet<PathKonsultasiEPaper> DaftarPathKonsultasiEPaper { get; set; }
        public DbSet<PathKonsultasiRepublika> DaftarPathKonsultasiRepublika { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artikel>().ToTable("Artikel");
            modelBuilder.Entity<Galeri>().ToTable("Galeri");
            modelBuilder.Entity<Infografis>().ToTable("Infografis");
            modelBuilder.Entity<KategoriArtikel>().ToTable("Kategori_Artikel");
            modelBuilder.Entity<KategoriKonsultasi>().ToTable("Kategori_Konsultasi");
            modelBuilder.Entity<KonsultasiEPaper>().ToTable("Konsultasi_EPaper");
            modelBuilder.Entity<KonsultasiRumahWasathia>().ToTable("Konsultasi_Rumah_Wasathia");
            modelBuilder.Entity<KonsultasiRepublika>().ToTable("Konsultasi_Republika");
            modelBuilder.Entity<PathArtikel>().ToTable("Path_Artikel");
            modelBuilder.Entity<PathGaleri>().ToTable("Path_Galeri");
            modelBuilder.Entity<PathInfografis>().ToTable("Path_Infografis");
            modelBuilder.Entity<PathKonsultasiEPaper>().ToTable("Path_Konsultasi_E_Paper");
            modelBuilder.Entity<PathKonsultasiRepublika>().ToTable("Path_Konsultasi_Republika");
        }

    }
}
