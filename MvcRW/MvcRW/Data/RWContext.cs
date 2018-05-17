using Microsoft.EntityFrameworkCore;
using MvcRW.Models;
using MySql.Data.MySqlClient;
using System;
using MvcRW.ViewModels;

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
        public DbSet<Admin> DaftarAdmin { get; set; }
        public DbSet<Artikel> DaftarArtikel { get; set; }
        public DbSet<Buku> DaftarBuku { get; set; }
        public DbSet<Galeri> DaftarGaleri { get; set; }
        public DbSet<KajianAudio> DaftarKajianAudio { get; set; }
        public DbSet<KajianVideo> DaftarKajianVideo { get; set; }
        public DbSet<KategoriArtikel> DaftarKategoriArtikel { get; set; }
        public DbSet<KategoriBuku> DaftarKategoriBuku { get; set; }
        public DbSet<KategoriKajian> DaftarKategoriKajian { get; set; }
        public DbSet<KategoriKonsultasi> DaftarKategoriKonsultasi { get; set; }
        public DbSet<KonsultasiEPaper> DaftarKonsultasiEPaper { get; set; }
        public DbSet<KonsultasiInfografis> DaftarKonsultasiInfografis { get; set; }
        public DbSet<KonsultasiRepublika> DaftarKonsultasiRepublika { get; set; }
        public DbSet<KonsultasiRumahWasathia> DaftarKonsultasiRumahWasathia { get; set; }    
        public DbSet<PathArtikel> DaftarPathArtikel { get; set; }
        public DbSet<PathBuku> DaftarPathBuku { get; set; }
        public DbSet<PathGaleri> DaftarPathGaleri { get; set; }
        public DbSet<PathKajianAudio> DaftarPathKajianAudio { get; set; }
        public DbSet<PathKajianVideo> DaftarPathKajianVideo { get; set; }
        public DbSet<PathKonsultasiEPaper> DaftarPathKonsultasiEPaper { get; set; }
        public DbSet<PathKonsultasiInfografis> DaftarPathKonsultasiInfografis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<Artikel>().ToTable("Artikel");
            modelBuilder.Entity<Buku>().ToTable("Buku");
            modelBuilder.Entity<Galeri>().ToTable("Galeri");
            modelBuilder.Entity<KajianAudio>().ToTable("Kajian_Audio");
            modelBuilder.Entity<KajianVideo>().ToTable("Kajian_Video");
            modelBuilder.Entity<KategoriArtikel>().ToTable("Kategori_Artikel");
            modelBuilder.Entity<KategoriBuku>().ToTable("Kategori_Buku");
            modelBuilder.Entity<KategoriKajian>().ToTable("Kategori_Kajian");
            modelBuilder.Entity<KategoriKonsultasi>().ToTable("Kategori_Konsultasi");
            modelBuilder.Entity<KategoriBuku>().ToTable("Kategori_Buku");
            modelBuilder.Entity<KategoriKonsultasi>().ToTable("Kategori_Konsultasi");
            modelBuilder.Entity<KonsultasiEPaper>().ToTable("Konsultasi_EPaper");
            modelBuilder.Entity<KonsultasiInfografis>().ToTable("Konsultasi_Infografis");
            modelBuilder.Entity<KonsultasiRepublika>().ToTable("Konsultasi_Republika");
            modelBuilder.Entity<KonsultasiRumahWasathia>().ToTable("Konsultasi_Rumah_Wasathia");
            modelBuilder.Entity<PathArtikel>().ToTable("Path_Artikel");
            modelBuilder.Entity<PathBuku>().ToTable("Path_Buku");
            modelBuilder.Entity<PathGaleri>().ToTable("Path_Galeri");
            modelBuilder.Entity<PathKajianAudio>().ToTable("Path_Kajian_Audio");
            modelBuilder.Entity<PathKajianVideo>().ToTable("Path_Kajian_Video");
            modelBuilder.Entity<PathKonsultasiEPaper>().ToTable("Path_Konsultasi_E_Paper");
            modelBuilder.Entity<PathKonsultasiInfografis>().ToTable("Path_Konsultasi_Infografis");
        }

    }
}
