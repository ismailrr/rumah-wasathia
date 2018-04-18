﻿using Microsoft.EntityFrameworkCore;
using MvcRW.Models;
using System;

namespace MvcRW.Data
{
    public class RWContext : DbContext
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
            modelBuilder.Entity<Artikel>().ToTable("Artikel");
            modelBuilder.Entity<Galeri>().ToTable("Galeri");
            modelBuilder.Entity<KategoriArtikel>().ToTable("Kategori_Artikel");
            modelBuilder.Entity<KategoriKonsultasi>().ToTable("Kategori_Konsultasi");
            modelBuilder.Entity<KonsultasiMedsos>().ToTable("Konsultasi_Medsos");
            modelBuilder.Entity<KonsultasiRepublika>().ToTable("Konsultasi_Republika");
            modelBuilder.Entity<PathArtikel>().ToTable("Path_Artikel");
            modelBuilder.Entity<PathGaleri>().ToTable("Path_Galeri");
            modelBuilder.Entity<PathKonsultasiRepublika>().ToTable("Path_Konsultasi_Republika");
        }

    }
}
