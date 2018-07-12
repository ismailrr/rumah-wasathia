using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcRWV2.Models;

namespace MvcRWV2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

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
        public DbSet<Tag> DaftarTag { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Admin>().ToTable("Admin");
            builder.Entity<Artikel>().ToTable("Artikel");
            builder.Entity<Buku>().ToTable("Buku");
            builder.Entity<Galeri>().ToTable("Galeri");
            builder.Entity<KajianAudio>().ToTable("Kajian_Audio");
            builder.Entity<KajianVideo>().ToTable("Kajian_Video");
            builder.Entity<KategoriArtikel>().ToTable("Kategori_Artikel");
            builder.Entity<KategoriBuku>().ToTable("Kategori_Buku");
            builder.Entity<KategoriKajian>().ToTable("Kategori_Kajian");
            builder.Entity<KategoriKonsultasi>().ToTable("Kategori_Konsultasi");
            builder.Entity<KategoriBuku>().ToTable("Kategori_Buku");
            builder.Entity<KategoriKonsultasi>().ToTable("Kategori_Konsultasi");
            builder.Entity<KonsultasiEPaper>().ToTable("Konsultasi_EPaper");
            builder.Entity<KonsultasiInfografis>().ToTable("Konsultasi_Infografis");
            builder.Entity<KonsultasiRepublika>().ToTable("Konsultasi_Republika");
            builder.Entity<KonsultasiRumahWasathia>().ToTable("Konsultasi_Rumah_Wasathia");
            builder.Entity<PathArtikel>().ToTable("Path_Artikel");
            builder.Entity<PathBuku>().ToTable("Path_Buku");
            builder.Entity<PathGaleri>().ToTable("Path_Galeri");
            builder.Entity<PathKajianAudio>().ToTable("Path_Kajian_Audio");
            builder.Entity<PathKajianVideo>().ToTable("Path_Kajian_Video");
            builder.Entity<PathKonsultasiEPaper>().ToTable("Path_Konsultasi_E_Paper");
            builder.Entity<PathKonsultasiInfografis>().ToTable("Path_Konsultasi_Infografis");
            builder.Entity<Tag>().ToTable("Tag");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
