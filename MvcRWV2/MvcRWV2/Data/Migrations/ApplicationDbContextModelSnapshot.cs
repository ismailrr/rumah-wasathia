﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MvcRWV2.Data;
using System;

namespace MvcRWV2.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MvcRWV2.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("KataSandi")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NamaPengguna")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("RememberMe");

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("MvcRWV2.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MvcRWV2.Models.Artikel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Judul")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("KategoriId");

                    b.Property<int?>("PathId");

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.HasIndex("PathId");

                    b.ToTable("Artikel");
                });

            modelBuilder.Entity("MvcRWV2.Models.Buku", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Deskripsi")
                        .HasMaxLength(5000);

                    b.Property<string>("ISBN")
                        .HasMaxLength(50);

                    b.Property<string>("Judul")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("KategoriId");

                    b.Property<int?>("PathId");

                    b.Property<string>("Penulis")
                        .HasMaxLength(200);

                    b.Property<DateTime>("Tanggal");

                    b.Property<int>("Tebal");

                    b.Property<string>("Terbitan")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.HasIndex("PathId");

                    b.ToTable("Buku");
                });

            modelBuilder.Entity("MvcRWV2.Models.Galeri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Judul")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("KategoriId");

                    b.Property<int>("PathId");

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.HasIndex("PathId");

                    b.ToTable("Galeri");
                });

            modelBuilder.Entity("MvcRWV2.Models.KajianAudio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("KategoriId");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int?>("PathId");

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.HasIndex("PathId");

                    b.ToTable("Kajian_Audio");
                });

            modelBuilder.Entity("MvcRWV2.Models.KajianVideo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("KategoriId");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int?>("PathId");

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.HasIndex("PathId");

                    b.ToTable("Kajian_Video");
                });

            modelBuilder.Entity("MvcRWV2.Models.KategoriArtikel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("Kategori_Artikel");
                });

            modelBuilder.Entity("MvcRWV2.Models.KategoriBuku", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("Kategori_Buku");
                });

            modelBuilder.Entity("MvcRWV2.Models.KategoriGaleri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("KategoriGaleri");
                });

            modelBuilder.Entity("MvcRWV2.Models.KategoriKajian", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("Kategori_Kajian");
                });

            modelBuilder.Entity("MvcRWV2.Models.KategoriKonsultasi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("Kategori_Konsultasi");
                });

            modelBuilder.Entity("MvcRWV2.Models.KonsultasiEPaper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Judul")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("KategoriId");

                    b.Property<int>("PathId");

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.HasIndex("PathId");

                    b.ToTable("Konsultasi_EPaper");
                });

            modelBuilder.Entity("MvcRWV2.Models.KonsultasiInfografis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Judul")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("KategoriId");

                    b.Property<int?>("PathId");

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.HasIndex("PathId");

                    b.ToTable("Konsultasi_Infografis");
                });

            modelBuilder.Entity("MvcRWV2.Models.KonsultasiRepublika", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Judul")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("KategoriId");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.ToTable("Konsultasi_Republika");
                });

            modelBuilder.Entity("MvcRWV2.Models.KonsultasiRumahWasathia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Jawaban")
                        .IsRequired()
                        .HasMaxLength(5000);

                    b.Property<string>("Judul")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("KategoriId");

                    b.Property<int?>("PathId");

                    b.Property<string>("Penulis")
                        .HasMaxLength(200);

                    b.Property<string>("Pertanyaan")
                        .IsRequired()
                        .HasMaxLength(5000);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.HasIndex("PathId");

                    b.ToTable("Konsultasi_Rumah_Wasathia");
                });

            modelBuilder.Entity("MvcRWV2.Models.PathArtikel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("Path_Artikel");
                });

            modelBuilder.Entity("MvcRWV2.Models.PathBuku", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("Path_Buku");
                });

            modelBuilder.Entity("MvcRWV2.Models.PathGaleri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("Path_Galeri");
                });

            modelBuilder.Entity("MvcRWV2.Models.PathKajianAudio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("Path_Kajian_Audio");
                });

            modelBuilder.Entity("MvcRWV2.Models.PathKajianVideo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("Path_Kajian_Video");
                });

            modelBuilder.Entity("MvcRWV2.Models.PathKonsultasiEPaper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("Path_Konsultasi_E_Paper");
                });

            modelBuilder.Entity("MvcRWV2.Models.PathKonsultasiInfografis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("Path_Konsultasi_Infografis");
                });

            modelBuilder.Entity("MvcRWV2.Models.PathKonsultasiRumahWasathia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("Tanggal");

                    b.HasKey("Id");

                    b.ToTable("PathKonsultasiRumahWasathia");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MvcRWV2.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MvcRWV2.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MvcRWV2.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MvcRWV2.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MvcRWV2.Models.Artikel", b =>
                {
                    b.HasOne("MvcRWV2.Models.KategoriArtikel", "Kategori")
                        .WithMany("DaftarArtikel")
                        .HasForeignKey("KategoriId");

                    b.HasOne("MvcRWV2.Models.PathArtikel", "Path")
                        .WithMany("DaftarArtikel")
                        .HasForeignKey("PathId");
                });

            modelBuilder.Entity("MvcRWV2.Models.Buku", b =>
                {
                    b.HasOne("MvcRWV2.Models.KategoriBuku", "Kategori")
                        .WithMany("DaftarBuku")
                        .HasForeignKey("KategoriId");

                    b.HasOne("MvcRWV2.Models.PathBuku", "Path")
                        .WithMany("DaftarBuku")
                        .HasForeignKey("PathId");
                });

            modelBuilder.Entity("MvcRWV2.Models.Galeri", b =>
                {
                    b.HasOne("MvcRWV2.Models.KategoriGaleri", "Kategori")
                        .WithMany("DaftarGaleri")
                        .HasForeignKey("KategoriId");

                    b.HasOne("MvcRWV2.Models.PathGaleri", "Path")
                        .WithMany("DaftarGaleri")
                        .HasForeignKey("PathId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MvcRWV2.Models.KajianAudio", b =>
                {
                    b.HasOne("MvcRWV2.Models.KategoriKajian", "Kategori")
                        .WithMany("DaftarKajianAudio")
                        .HasForeignKey("KategoriId");

                    b.HasOne("MvcRWV2.Models.PathKajianAudio", "Path")
                        .WithMany("DaftarKajianAudio")
                        .HasForeignKey("PathId");
                });

            modelBuilder.Entity("MvcRWV2.Models.KajianVideo", b =>
                {
                    b.HasOne("MvcRWV2.Models.KategoriKajian", "Kategori")
                        .WithMany("DaftarKajianVideo")
                        .HasForeignKey("KategoriId");

                    b.HasOne("MvcRWV2.Models.PathKajianVideo", "Path")
                        .WithMany("DaftarKajianVideo")
                        .HasForeignKey("PathId");
                });

            modelBuilder.Entity("MvcRWV2.Models.KonsultasiEPaper", b =>
                {
                    b.HasOne("MvcRWV2.Models.KategoriKonsultasi", "Kategori")
                        .WithMany("DaftarKonsultasiEPaper")
                        .HasForeignKey("KategoriId");

                    b.HasOne("MvcRWV2.Models.PathKonsultasiEPaper", "Path")
                        .WithMany("DaftarKonsultasiEPaper")
                        .HasForeignKey("PathId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MvcRWV2.Models.KonsultasiInfografis", b =>
                {
                    b.HasOne("MvcRWV2.Models.KategoriKonsultasi", "Kategori")
                        .WithMany("DaftarKonsultasiInfografis")
                        .HasForeignKey("KategoriId");

                    b.HasOne("MvcRWV2.Models.PathKonsultasiInfografis", "Path")
                        .WithMany("DaftarKonsultasiInfografis")
                        .HasForeignKey("PathId");
                });

            modelBuilder.Entity("MvcRWV2.Models.KonsultasiRepublika", b =>
                {
                    b.HasOne("MvcRWV2.Models.KategoriKonsultasi", "Kategori")
                        .WithMany("DaftarKonsultasiRepublika")
                        .HasForeignKey("KategoriId");
                });

            modelBuilder.Entity("MvcRWV2.Models.KonsultasiRumahWasathia", b =>
                {
                    b.HasOne("MvcRWV2.Models.KategoriKonsultasi", "Kategori")
                        .WithMany("DaftarKonsultasiRumahWasathia")
                        .HasForeignKey("KategoriId");

                    b.HasOne("MvcRWV2.Models.PathKonsultasiRumahWasathia", "Path")
                        .WithMany("DaftarkonsultasiRumahWasathia")
                        .HasForeignKey("PathId");
                });
#pragma warning restore 612, 618
        }
    }
}
