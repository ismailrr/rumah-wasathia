using MvcRW.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MvcRW.Data
{
    /// <summary>
    /// Custom database initializer class used to populate
    /// the database with seed data.
    /// </summary>
    public static class DbInitializer
    {
        public static void Initialize(RWContext context)
        {
            context.Database.EnsureCreated();

            if (context.DaftarArtikel.Any())
            {
                return;
            }
            var ext = new List<string> { "pdf" };
            var myFiles = Directory.GetFiles("~/rwtheme/file-document/article/", "*.*", SearchOption.AllDirectories)
                 .Where(s => ext.Contains(Path.GetExtension(s)));
            var pathArtikel = new PathArtikel[]
            {
            new PathArtikel{Path=myFiles[0]},
            new PathArtikel{Path="~/rwtheme/pdf/37-masalah-populer.pdf"},
            new PathArtikel{Path="~/rwtheme/pdf/37-masalah-populer.pdf"},
            new PathArtikel{Path="~/rwtheme/pdf/37-masalah-populer.pdf"},
            new PathArtikel{Path="~/rwtheme/pdf/37-masalah-populer.pdf"},
            new PathArtikel{Path="~/rwtheme/pdf/37-masalah-populer.pdf"},
            new PathArtikel{Path="~/rwtheme/pdf/37-masalah-populer.pdf"},
            new PathArtikel{Path="~/rwtheme/pdf/37-masalah-populer.pdf"},
            new PathArtikel{Path="~/rwtheme/pdf/37-masalah-populer.pdf"}
            };
            foreach (PathArtikel a in pathArtikel)
            {
                context.DaftarPathArtikel.Add(a);
            }
            context.SaveChanges();

            var pathGaleri = new PathGaleri[]
            {
            new PathGaleri{Path="~/rwtheme/images/sec/sec-1.jpg"},
            new PathGaleri{Path="~/rwtheme/images/sec/sec-2.jpg"},
            new PathGaleri{Path="~/rwtheme/images/sec/sec-1.jpg"},
            new PathGaleri{Path="~/rwtheme/images/sec/sec-2.jpg"},
            new PathGaleri{Path="~/rwtheme/images/sec/sec-1.jpg"},
            new PathGaleri{Path="~/rwtheme/images/sec/sec-2.jpg"},
            new PathGaleri{Path="~/rwtheme/images/sec/sec-1.jpg"},
            new PathGaleri{Path="~/rwtheme/images/sec/sec-2.jpg"},
            new PathGaleri{Path="~/rwtheme/images/sec/sec-1.jpg"},
            new PathGaleri{Path="~/rwtheme/images/sec/sec-2.jpg"},
            new PathGaleri{Path="~/rwtheme/images/sec/sec-1.jpg"},
            new PathGaleri{Path="~/rwtheme/images/sec/sec-2.jpg"}
            };
            foreach (PathGaleri a in pathGaleri)
            {
                context.DaftarPathGaleri.Add(a);
            }
            context.SaveChanges();

            var pathKonsultasiRepublika = new PathKonsultasiRepublika[]
            {
            new PathKonsultasiRepublika{Path="~/rwtheme/images/e-paper/1. Perjalanan Dinas yang dipersingkat.jpg"},
            new PathKonsultasiRepublika{Path="~/rwtheme/images/e-paper/2. Bisnis Reseller.jpg"},
            new PathKonsultasiRepublika{Path="~/rwtheme/images/e-paper/3. Dropship.jpeg"},
            new PathKonsultasiRepublika{Path="~/rwtheme/images/e-paper/1. Perjalanan Dinas yang dipersingkat.jpg"},
            new PathKonsultasiRepublika{Path="~/rwtheme/images/e-paper/2. Bisnis Reseller.jpg"},
            new PathKonsultasiRepublika{Path="~/rwtheme/images/e-paper/3. Dropship.jpeg"}
            };
            foreach (PathKonsultasiRepublika a in pathKonsultasiRepublika)
            {
                context.DaftarPathKonsultasiRepublika.Add(a);
            }
            context.SaveChanges();

            var kategoriArtikel = new KategoriArtikel[]
            {
            new KategoriArtikel{Nama="Bisnis Online"},
            new KategoriArtikel{Nama="Bisnis"},
            new KategoriArtikel{Nama="Syariah"},
            new KategoriArtikel{Nama="Fikih"},
            new KategoriArtikel{Nama="Muamalah"},
            new KategoriArtikel{Nama="Hadist"},
            new KategoriArtikel{Nama="Alquran"}
            };
            foreach (KategoriArtikel a in kategoriArtikel)
            {
                context.DaftarKategoriArtikel.Add(a);
            }
            context.SaveChanges();

            var kategoriKonsultasi = new KategoriKonsultasi[]
            {
            new KategoriKonsultasi{Nama="Bisnis Online"},
            new KategoriKonsultasi{Nama="Bisnis"},
            new KategoriKonsultasi{Nama="Syariah"},
            new KategoriKonsultasi{Nama="Fikih"},
            new KategoriKonsultasi{Nama="Muamalah"},
            new KategoriKonsultasi{Nama="Hadist"},
            new KategoriKonsultasi{Nama="Alquran"}
            };
            foreach (KategoriKonsultasi a in kategoriKonsultasi)
            {
                context.DaftarKategoriKonsultasi.Add(a);
            }
            context.SaveChanges();

            var artikel = new Artikel[]
            {
            new Artikel{Judul="PDF 1",Tanggal=DateTime.Parse("2018-03-08"),Path=pathArtikel[0]},
            new Artikel{Judul="PDF 2",Tanggal=DateTime.Parse("2018-04-07"),Path=pathArtikel[1]},
            new Artikel{Judul="PDF 3",Tanggal=DateTime.Parse("2018-02-08"),Path=pathArtikel[2]},
            new Artikel{Judul="PDF 4",Tanggal=DateTime.Parse("2018-04-10"),Path=pathArtikel[3]},
            new Artikel{Judul="PDF 5",Tanggal=DateTime.Parse("2018-04-01"),Path=pathArtikel[4]},
            new Artikel{Judul="PDF 6",Tanggal=DateTime.Parse("2018-03-15"),Path=pathArtikel[5]},
            new Artikel{Judul="PDF 7",Tanggal=DateTime.Parse("2018-03-11"),Path=pathArtikel[6]},
            new Artikel{Judul="PDF 8",Tanggal=DateTime.Parse("2018-02-02"),Path=pathArtikel[1]},
            new Artikel{Judul="PDF 9",Tanggal=DateTime.Parse("2018-03-21"),Path=pathArtikel[5]}
            };
            foreach (Artikel a in artikel)
            {
                context.DaftarArtikel.Add(a);
            }
            context.SaveChanges();

            var galeri = new Galeri[]
            {
            new Galeri{Judul="Galeri 1",Tanggal=DateTime.Parse("2018-03-08"),Path=pathGaleri[0]},
            new Galeri{Judul="Galeri 2",Tanggal=DateTime.Parse("2018-04-07"),Path=pathGaleri[1]},
            new Galeri{Judul="Galeri 3",Tanggal=DateTime.Parse("2018-02-08"),Path=pathGaleri[2]},
            new Galeri{Judul="Galeri 4",Tanggal=DateTime.Parse("2018-04-10"),Path=pathGaleri[3]},
            new Galeri{Judul="Galeri 5",Tanggal=DateTime.Parse("2018-04-01"),Path=pathGaleri[4]},
            new Galeri{Judul="Galeri 6",Tanggal=DateTime.Parse("2018-03-15"),Path=pathGaleri[5]},
            new Galeri{Judul="Galeri 7",Tanggal=DateTime.Parse("2018-03-11"),Path=pathGaleri[6]},
            new Galeri{Judul="Galeri 8",Tanggal=DateTime.Parse("2018-02-02"),Path=pathGaleri[5]},
            new Galeri{Judul="Galeri 9",Tanggal=DateTime.Parse("2018-03-21"),Path=pathGaleri[2]}
            };
            foreach (Galeri a in galeri)
            {
                context.DaftarGaleri.Add(a);
            }
            context.SaveChanges();

            var konsultasiMedsos = new KonsultasiMedsos[]
            {
            new KonsultasiMedsos{Judul="1.Lorem Ipsum is simply dummy text",Tanggal=DateTime.Parse("2018-03-08"),Kategori=kategoriKonsultasi[0],Pertanyaan="Lorem Ipsum is simply dummy text",Jawaban="Lorem Ipsum is simply dummy text"},
            new KonsultasiMedsos{Judul="2.Lorem Ipsum is simply dummy text",Tanggal=DateTime.Parse("2018-04-07"),Kategori=kategoriKonsultasi[1],Pertanyaan="Lorem Ipsum is simply dummy text",Jawaban="Lorem Ipsum is simply dummy text"},
            new KonsultasiMedsos{Judul="3.Lorem Ipsum is simply dummy text",Tanggal=DateTime.Parse("2018-02-08"),Kategori=kategoriKonsultasi[2],Pertanyaan="Lorem Ipsum is simply dummy text",Jawaban="Lorem Ipsum is simply dummy text"},
            new KonsultasiMedsos{Judul="4.Lorem Ipsum is simply dummy text",Tanggal=DateTime.Parse("2018-04-10"),Kategori=kategoriKonsultasi[3],Pertanyaan="Lorem Ipsum is simply dummy text",Jawaban="Lorem Ipsum is simply dummy text"},
            new KonsultasiMedsos{Judul="5.Lorem Ipsum is simply dummy text",Tanggal=DateTime.Parse("2018-04-01"),Kategori=kategoriKonsultasi[4],Pertanyaan="Lorem Ipsum is simply dummy text",Jawaban="Lorem Ipsum is simply dummy text"},
            new KonsultasiMedsos{Judul="6.Lorem Ipsum is simply dummy text",Tanggal=DateTime.Parse("2018-03-15"),Kategori=kategoriKonsultasi[5],Pertanyaan="Lorem Ipsum is simply dummy text",Jawaban="Lorem Ipsum is simply dummy text"},
            new KonsultasiMedsos{Judul="7.Lorem Ipsum is simply dummy text",Tanggal=DateTime.Parse("2018-03-11"),Kategori=kategoriKonsultasi[5],Pertanyaan="Lorem Ipsum is simply dummy text",Jawaban="Lorem Ipsum is simply dummy text"},
            new KonsultasiMedsos{Judul="8.Lorem Ipsum is simply dummy text",Tanggal=DateTime.Parse("2018-02-02"),Kategori=kategoriKonsultasi[3],Pertanyaan="Lorem Ipsum is simply dummy text",Jawaban="Lorem Ipsum is simply dummy text"},
            new KonsultasiMedsos{Judul="9.Lorem Ipsum is simply dummy text",Tanggal=DateTime.Parse("2018-03-21"),Kategori=kategoriKonsultasi[2],Pertanyaan="Lorem Ipsum is simply dummy text",Jawaban="Lorem Ipsum is simply dummy text"}
            };
            foreach (KonsultasiMedsos a in konsultasiMedsos)
            {
                context.DaftarKonsultasiMedsos.Add(a);
            }
            context.SaveChanges();

            var konsultasiRepublika = new KonsultasiRepublika[]
            {
            new KonsultasiRepublika{Judul="PDF 1",Tanggal=DateTime.Parse("2018-03-08"),Kategori=kategoriKonsultasi[0],Path=pathKonsultasiRepublika[4]},
            new KonsultasiRepublika{Judul="PDF 2",Tanggal=DateTime.Parse("2018-04-07"),Kategori=kategoriKonsultasi[1],Path=pathKonsultasiRepublika[5]},
            new KonsultasiRepublika{Judul="PDF 3",Tanggal=DateTime.Parse("2018-02-08"),Kategori=kategoriKonsultasi[2],Path=pathKonsultasiRepublika[0]},
            new KonsultasiRepublika{Judul="PDF 4",Tanggal=DateTime.Parse("2018-04-10"),Kategori=kategoriKonsultasi[3],Path=pathKonsultasiRepublika[5]},
            new KonsultasiRepublika{Judul="PDF 5",Tanggal=DateTime.Parse("2018-04-01"),Kategori=kategoriKonsultasi[4],Path=pathKonsultasiRepublika[1]},
            new KonsultasiRepublika{Judul="PDF 6",Tanggal=DateTime.Parse("2018-03-15"),Kategori=kategoriKonsultasi[5],Path=pathKonsultasiRepublika[1]},
            new KonsultasiRepublika{Judul="PDF 7",Tanggal=DateTime.Parse("2018-03-11"),Kategori=kategoriKonsultasi[5],Path=pathKonsultasiRepublika[3]},
            new KonsultasiRepublika{Judul="PDF 8",Tanggal=DateTime.Parse("2018-02-02"),Kategori=kategoriKonsultasi[3],Path=pathKonsultasiRepublika[2]},
            new KonsultasiRepublika{Judul="PDF 9",Tanggal=DateTime.Parse("2018-03-21"),Kategori=kategoriKonsultasi[2],Path=pathKonsultasiRepublika[5]}
            };
            foreach (KonsultasiRepublika a in konsultasiRepublika)
            {
                context.DaftarKonsultasiRepublika.Add(a);
            }
            context.SaveChanges();

        }
    }
}
