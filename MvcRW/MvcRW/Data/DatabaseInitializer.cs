using MvcRW.Models;
using System;
using System.Linq;

namespace MvcRW.Data
{
    /// <summary>
    /// Custom database initializer class used to populate
    /// the database with seed data.
    /// </summary>
    internal class DatabaseInitializer
    {
        protected void Seed(RWContext context)
        {
            context.Database.EnsureCreated();

            if (context.DaftarArtikel.Any())
            {
                return;
            }

            var pathArtikel = new PathArtikel[]
            {
            new PathArtikel{Path="~/rwtheme/pdf/37-masalah-populer.pdf"},
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

            var artikel = new Artikel[]
            {
            new Artikel{Judul="PDF 1",Tanggal=DateTime.Parse("2018-03-08"),Path=pathArtikel[0]},
            new Artikel{Judul="PDF 2",Tanggal=DateTime.Parse("2018-04-07"),Path=pathArtikel[1]},
            new Artikel{Judul="PDF 3",Tanggal=DateTime.Parse("2018-02-08"),Path=pathArtikel[2]},
            new Artikel{Judul="PDF 4",Tanggal=DateTime.Parse("2018-04-10"),Path=pathArtikel[3]},
            new Artikel{Judul="PDF 5",Tanggal=DateTime.Parse("2018-04-01"),Path=pathArtikel[4]},
            new Artikel{Judul="PDF 6",Tanggal=DateTime.Parse("2018-03-15"),Path=pathArtikel[5]},
            new Artikel{Judul="PDF 7",Tanggal=DateTime.Parse("2018-03-11"),Path=pathArtikel[6]},
            new Artikel{Judul="PDF 8",Tanggal=DateTime.Parse("2018-02-02"),Path=pathArtikel[7]},
            new Artikel{Judul="PDF 9",Tanggal=DateTime.Parse("2018-03-21"),Path=pathArtikel[8]}
            };
            foreach (Artikel a in artikel)
            {
                context.DaftarArtikel.Add(a);
            }
            context.SaveChanges();
        }
    }
}
