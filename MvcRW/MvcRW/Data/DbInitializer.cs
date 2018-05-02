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

            /* Artikel */
            //var ext = new List<string> { "pdf" };
            var myFilesArtikel = Directory.GetFiles("wwwroot/rwtheme/file-document/article/", "*.pdf");

            var pathArtikel = new List<PathArtikel>();

            foreach (string s in myFilesArtikel.Select(Path.GetFileName))
            {
                string PathString = s;
                pathArtikel.Add(new PathArtikel { Path = PathString });
            }

            foreach (PathArtikel a in pathArtikel)
            {
                context.DaftarPathArtikel.Add(a);
            }
            context.SaveChanges();

            /* Galeri */
            var extGambar = new List<string> { ".jpeg",".jpg",".png",".webp"};
            var myFilesGambar = Directory.GetFiles("wwwroot/rwtheme/images/galeri/", "*.*", SearchOption.AllDirectories).Where(s => extGambar.Contains(Path.GetExtension(s)));

            var pathGaleri = new List<PathGaleri>();

            foreach (string s in myFilesGambar.Select(Path.GetFileName))
            {
                string PathString = s;
                pathGaleri.Add(new PathGaleri { Path = PathString });
            }

            foreach (PathGaleri a in pathGaleri)
            {
                context.DaftarPathGaleri.Add(a);
            }
            context.SaveChanges();

            /* Infografis */
            //var ext = new List<string> { "pdf" };
            var myFilesInfografis = Directory.GetFiles("wwwroot/rwtheme/file-document/pdf/", "*.pdf");

            var pathInfografis = new List<PathInfografis>();

            foreach (string s in myFilesInfografis.Select(Path.GetFileName))
            {
                string PathString = s;
                pathInfografis.Add(new PathInfografis { Path = PathString });
            }

            foreach (PathInfografis a in pathInfografis)
            {
                context.DaftarPathInfografis.Add(a);
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

            /*Artikel*/
            var artikel = new List<Artikel>();

            foreach (PathArtikel s in pathArtikel)
            {
                string MyString = s.Path.ToString();
                string JudulString = MyString.Replace(".pdf","");
                artikel.Add(new Artikel { Judul = JudulString, Tanggal = DateTime.Parse("2018-03-08"), Path = s });
            }

            foreach (Artikel a in artikel)
            {
                context.DaftarArtikel.Add(a);
            }
            context.SaveChanges();

            /* Galeri */
            var galeri = new List<Galeri>();

            foreach (PathGaleri s in pathGaleri)
            {
                string MyString = s.Path.ToString();
                int index = MyString.LastIndexOf(".");
                string JudulString = MyString.Replace(".pdf", "");
                galeri.Add(new Galeri { Judul = JudulString, Tanggal = DateTime.Parse("2018-03-08"), Path = s });
            }

            foreach (Galeri a in galeri)
            {
                context.DaftarGaleri.Add(a);
            }
            context.SaveChanges();

            /* Infografis */
            var infografis = new List<Infografis>();

            foreach (PathInfografis s in pathInfografis)
            {
                string MyString = s.Path.ToString();
                string JudulString = MyString.Replace(".pdf", "");
                infografis.Add(new Infografis { Judul = JudulString, Tanggal = DateTime.Parse("2018-03-08"), Path = s });
            }

            foreach (Infografis a in infografis)
            {
                context.DaftarInfografis.Add(a);
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
