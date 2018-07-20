using MvcRWV2.Models;
using MvcRWV2.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MvcRWV2.Data
{
    /// <summary>
    /// Custom database initializer class used to populate
    /// the database with seed data.
    /// </summary>
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            var extGambar = new List<string> { ".jpeg", ".jpg", ".png", ".webp" };
            var extPdf = new List<string> { ".pdf"};

            //Cek jika data sudah dibuat
            if (context.DaftarArtikel.Any() || context.DaftarBuku.Any())
            {
                return;
            }

            /* 
             * Path Artikel 
             */
            var myFilesArtikel = Directory.GetFiles("wwwroot/uploads/pdf/artikel/", "*.*", SearchOption.AllDirectories).Where(s => extPdf.Contains(Path.GetExtension(s)));

            var pathArtikel = new List<PathArtikel>();

            foreach (string s in myFilesArtikel.Select(Path.GetFileName))
            {
                string PathString = s;
                pathArtikel.Add(new PathArtikel {Path = "/uploads/pdf/artikel/"+PathString, Tanggal = DateTime.Now});
            }

            foreach (PathArtikel a in pathArtikel)
            {
                context.DaftarPathArtikel.Add(a);
            }
            context.SaveChanges();

            /* 
             * Path Buku 
             */
            var myFilesBuku = Directory.GetFiles("wwwroot/uploads/image/book/", "*.*", SearchOption.AllDirectories).Where(s => extGambar.Contains(Path.GetExtension(s)));

            var pathBuku = new List<PathBuku>();

            foreach (string s in myFilesBuku.Select(Path.GetFileName))
            {
                string PathString = s;
                pathBuku.Add(new PathBuku {Path = "/uploads/image/book/" + PathString, Tanggal = DateTime.Now});
            }

            foreach (PathBuku a in pathBuku)
            {
                context.DaftarPathBuku.Add(a);
            }
            context.SaveChanges();

            /* 
             * Path Galeri 
             */
            var myFilesGaleri = Directory.GetFiles("wwwroot/uploads/image/galeri/", "*.*", SearchOption.AllDirectories).Where(s => extGambar.Contains(Path.GetExtension(s)));

            var pathGaleri = new List<PathGaleri>();

            foreach (string s in myFilesGaleri.Select(Path.GetFileName))
            {
                string PathString = s;
                pathGaleri.Add(new PathGaleri {Path = "/uploads/image/galeri/" + PathString, Tanggal = DateTime.Now });
            }

            foreach (PathGaleri a in pathGaleri)
            {
                context.DaftarPathGaleri.Add(a);
            }
            context.SaveChanges();

            /* 
             * Path Infografis 
             */
            var myFilesInfografis = Directory.GetFiles("wwwroot/uploads/pdf/infografis/", "*.*", SearchOption.AllDirectories).Where(s => extPdf.Contains(Path.GetExtension(s)));

            var pathInfografis = new List<PathKonsultasiInfografis>();

            foreach (string s in myFilesInfografis.Select(Path.GetFileName))
            {
                string PathString = s;
                pathInfografis.Add(new PathKonsultasiInfografis {Path = "/uploads/pdf/infografis/" + PathString, Tanggal = DateTime.Now});
            }

            foreach (PathKonsultasiInfografis a in pathInfografis)
            {
                context.DaftarPathKonsultasiInfografis.Add(a);
            }
            context.SaveChanges();

            /* 
             * Path KajianAudio 
             */
            var pathKajianAudio = new PathKajianAudio[]
            {
            new PathKajianAudio{Path = "",Tanggal=DateTime.Now}
            };

            foreach (PathKajianAudio a in pathKajianAudio)
            {
                context.DaftarPathKajianAudio.Add(a);
            }
            context.SaveChanges();

            /* 
             * Path KajianVideo 
             */
            var pathKajianVideo = new PathKajianVideo[]
            {
            new PathKajianVideo{Path = "",Tanggal=DateTime.Now}
            };

            foreach (PathKajianVideo a in pathKajianVideo)
            {
                context.DaftarPathKajianVideo.Add(a);
            }
            context.SaveChanges();

            /* 
             * Path Konsultasi E Paper 
             */
            var myFilesEPaper = Directory.GetFiles("wwwroot/uploads/image/epaper/", "*.*", SearchOption.AllDirectories).Where(s => extGambar.Contains(Path.GetExtension(s)));

            var pathKonsultasiEPaper = new List<PathKonsultasiEPaper>();

            foreach (string s in myFilesEPaper.Select(Path.GetFileName))
            {
                string PathString = s;
                pathKonsultasiEPaper.Add(new PathKonsultasiEPaper {Path = "/uploads/image/epaper/" + PathString, Tanggal = DateTime.Now});
            }

            foreach (PathKonsultasiEPaper a in pathKonsultasiEPaper)
            {
                context.DaftarPathKonsultasiEPaper.Add(a);
            }
            context.SaveChanges();

            /* 
             * Katgeori Artikel 
             */
            var kategoriArtikel = new KategoriArtikel[]
            {
            new KategoriArtikel{Nama="Bisnis Online",Tanggal=DateTime.Now},
            new KategoriArtikel{Nama="Bisnis",Tanggal=DateTime.Now},
            new KategoriArtikel{Nama="Syariah",Tanggal=DateTime.Now},
            new KategoriArtikel{Nama="Fikih",Tanggal=DateTime.Now},
            new KategoriArtikel{Nama="Muamalah",Tanggal=DateTime.Now},
            new KategoriArtikel{Nama="Hadist",Tanggal=DateTime.Now},
            new KategoriArtikel{Nama="Alquran",Tanggal=DateTime.Now }
            };
            foreach (KategoriArtikel a in kategoriArtikel)
            {
                context.DaftarKategoriArtikel.Add(a);
            }
            context.SaveChanges();

            /* 
             * Katgeori Buku 
             */
            var kategoriBuku = new KategoriBuku[]
            {
            new KategoriBuku{Nama="Bisnis Online",Tanggal=DateTime.Now},
            new KategoriBuku{Nama="Bisnis",Tanggal=DateTime.Now},
            new KategoriBuku{Nama="Syariah",Tanggal=DateTime.Now},
            new KategoriBuku{Nama="Fikih",Tanggal=DateTime.Now},
            new KategoriBuku{Nama="Muamalah",Tanggal=DateTime.Now},
            new KategoriBuku{Nama="Hadist",Tanggal=DateTime.Now},
            new KategoriBuku{Nama="Alquran",Tanggal=DateTime.Now}
            };
            foreach (KategoriBuku a in kategoriBuku)
            {
                context.DaftarKategoriBuku.Add(a);
            }
            context.SaveChanges();

            /* 
             * Katgeori Kajian 
             */
            var kategoriKajian = new KategoriKajian[]
            {
            new KategoriKajian{Nama="Bisnis Online",Tanggal=DateTime.Now},
            new KategoriKajian{Nama="Bisnis",Tanggal=DateTime.Now},
            new KategoriKajian{Nama="Syariah",Tanggal=DateTime.Now},
            new KategoriKajian{Nama="Fikih",Tanggal=DateTime.Now},
            new KategoriKajian{Nama="Muamalah",Tanggal=DateTime.Now},
            new KategoriKajian{Nama="Hadist",Tanggal=DateTime.Now},
            new KategoriKajian{Nama="Alquran",Tanggal=DateTime.Now}
            };
            foreach (KategoriKajian a in kategoriKajian)
            {
                context.DaftarKategoriKajian.Add(a);
            }
            context.SaveChanges();

            /* 
             * Katgeori Konsultasi 
             */
            var kategoriKonsultasi = new KategoriKonsultasi[]
            {
            new KategoriKonsultasi{Nama="Bisnis Online",Tanggal=DateTime.Now},
            new KategoriKonsultasi{Nama="Bisnis",Tanggal=DateTime.Now},
            new KategoriKonsultasi{Nama="Syariah",Tanggal=DateTime.Now},
            new KategoriKonsultasi{Nama="Fikih",Tanggal=DateTime.Now},
            new KategoriKonsultasi{Nama="Muamalah",Tanggal=DateTime.Now},
            new KategoriKonsultasi{Nama="Hadist",Tanggal=DateTime.Now},
            new KategoriKonsultasi{Nama="Alquran",Tanggal=DateTime.Now}
            };
            foreach (KategoriKonsultasi a in kategoriKonsultasi)
            {
                context.DaftarKategoriKonsultasi.Add(a);
            }
            context.SaveChanges();

            /*
             * Artikel
             
            var artikel = new List<Artikel>();

            foreach (PathArtikel s in pathArtikel)
            {
                string MyString = s.Path.ToString();
                string JudulString = MyString.Replace(".pdf","");
                JudulString = JudulString.Replace("/uploads/pdf/artikel/", "");
                artikel.Add(new Artikel { Judul = JudulString, Tanggal = DateTime.Now, Path = s, Status = 1,Penulis="admin",Source = s.Path, FImage = "/uploads/image/general/pdf.png"});
            }
            
            foreach (Artikel a in artikel)
            {
                context.DaftarArtikel.Add(a);
            }
            context.SaveChanges();
            
            List<GoogleDriveFiles> FileList = GoogleDriveFilesRepository.GetDriveFilesArtikel();
            var artikel = new List<Artikel>();
            foreach (var file in FileList)
            {
                artikel.Add(new Artikel { Judul = file.Name, Tanggal = file.CreatedTime, Status = 1, Penulis = "admin", Source = file.WebContentLink, FImage = "/uploads/image/general/pdf.png" });
            }
            foreach (Artikel a in artikel)
            {
                context.DaftarArtikel.Add(a);
            }
            context.SaveChanges();
            */

            /*
             * Buku
             */
            var buku = new List<Buku>();

            foreach (PathBuku s in pathBuku)
            {
                string MyString = s.Path.ToString();
                string JudulString = MyString.Replace(".pdf", "");
                JudulString = JudulString.Replace("/uploads/image/book/", "");
                buku.Add(new Buku { Judul = JudulString, PenulisBuku = "", Terbitan = "", ISBN = "", Deskripsi ="", Tebal = 1, Tanggal = DateTime.Now, Path = s, Status = 1, Penulis = "admin", FImage = s.Path });
            }

            foreach (Buku a in buku)
            {
                context.DaftarBuku.Add(a);
            }
            context.SaveChanges();

            /*
             * Galeri 
             */
            var galeri = new List<Galeri>();

            foreach (PathGaleri s in pathGaleri)
            {
                string MyString = s.Path.ToString();
                //int index = MyString.LastIndexOf(".");
                string JudulString = MyString.Replace(".pdf", "");
                JudulString = JudulString.Replace("/uploads/image/galeri/",  "");
                galeri.Add(new Galeri { Judul = JudulString, Tanggal = DateTime.Now, Path = s, Source = s.Path, FImage = s.Path, Status = 1 });
            }
            var galeri2 = new List<Galeri>();
            galeri2.Add(new Galeri { Judul = "Galeri 1", Tanggal = DateTime.Now, Status = 1});
            for (int i = 0;i<galeri.Count-1;i++)
            {
                galeri2[0].Source += galeri[i].Path.Path + "\n";
                if (i == galeri.Count-1)
                {
                    galeri2[0].FImage += galeri[i].Path.Path + "\n";
                }
                
            }
            foreach (Galeri a in galeri2)
            {
                context.DaftarGaleri.Add(a);
            }
            context.SaveChanges();

            /* 
             * Infografis 
             */
            var infografis = new List<KonsultasiInfografis>();

            foreach (PathKonsultasiInfografis s in pathInfografis)
            {
                string MyString = s.Path.ToString();
                string JudulString = MyString.Replace(".pdf", "");
                JudulString = JudulString.Replace("/uploads/pdf/infografis/", "");
                infografis.Add(new KonsultasiInfografis { Judul = JudulString, Tanggal = DateTime.Now, Path = s, Status = 1, Penulis = "admin", Source = s.Path, FImage = "/uploads/image/general/chart.png" });
            }

            foreach (KonsultasiInfografis a in infografis)
            {
                context.DaftarKonsultasiInfografis.Add(a);
            }
            context.SaveChanges();

            /* 
             * Kajian Audio 
             */
            var kajianAudio = new KajianAudio[]
            {
                new KajianAudio{ Source = "https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/357475985&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true", Tanggal = DateTime.Now, Status = 1,Penulis="admin"},
                new KajianAudio{ Source = "https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/268727142&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianAudio{ Source = "https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/playlists/224844275&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianAudio{ Source = "https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/292675007&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianAudio{ Source = "https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/playlists/224844275&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true", Tanggal = DateTime.Now , Status = 1,Penulis="admin"},
                new KajianAudio{ Source = "https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/212773651&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianAudio{ Source = "https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/413160264&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianAudio{ Source = "https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/196296200&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true", Tanggal = DateTime.Now, Status = 1,Penulis="admin", Judul = "aaaaaaaaaaaaaa"  }
            };
            
            //var kajianAudio = new List<KajianAudio>();

            /*
            foreach (PathKajianAudio s in pathKajianAudio)
            {
                string MyString = s.Path.ToString();
                string JudulString = MyString.Replace(".pdf", "");
                kajianAudio.Add(new KajianAudio { Source = "https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/357475985&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true", Tanggal = DateTime.Now, Path = s });
            }*/

            foreach (KajianAudio a in kajianAudio)
            {
                a.FImage = a.Source;
                context.DaftarKajianAudio.Add(a);
            }
            context.SaveChanges();

            /* 
             * Kajian Video 
             */
            var kajianVideo = new KajianVideo[]
            {
                new KajianVideo{ Source = "https://www.youtube.com/embed/NhCwuonQaMA", Tanggal = DateTime.Now, Status = 1,Penulis="admin"},
                new KajianVideo{ Source = "https://www.youtube.com/embed/INK1IvY4WSA", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/HXOy6M8Vedk", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/8bPY1JfdHoo", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/RUaYn6sogWA", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/veSwehsY0-Q", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/cVsrwXnQytM", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/qggLZOQS87U", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/Rqe8OWIOm0w", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/tRlsZGYqc14", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/qtV6syQ6gAY", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/8BrivQ_Qgm4", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/JckLDRJ1VSE", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/dZeLwE_1pao", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/N2sE_WJJeHM", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/7_TO7NfwVsE", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/083V4Njtmsk", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/5rcYyusDdHk", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/ASg2un67amU", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/AxSr716O0rE", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/GqCh2wyO_J0", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/TZCHofmT-jg", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/FDIU0GCyNqE", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/S0QswxWoazs", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/oK0_M5pnpqg", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/-EVhTEnfEJs", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/gTpf2BoMZq0", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/QxbyH80YlZk", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/wNlUv40LWRQ", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/oPyR7aKwn_8", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/BkY3zK14b4k", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/7KiO2FjOJkU", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/YleGMu9qILE", Tanggal = DateTime.Now, Status = 1,Penulis="admin" },
                new KajianVideo{ Source = "https://www.youtube.com/embed/4E-oEEvw5G8", Tanggal = DateTime.Now, Status = 1,Penulis="admin", Judul = "aaaaaaaaaaaaaa"  }
            };

            

            //var kajianVideo = new List<KajianVideo>();
            /*
            foreach (PathKajianVideo s in pathKajianVideo)
            {
                string MyString = s.Path.ToString();
                string JudulString = MyString.Replace(".pdf", "");
                kajianVideo.Add(new KajianVideo { Source = "https://www.youtube.com/embed/NhCwuonQaMA", Tanggal = DateTime.Now, Path = s });
            }
            */

            foreach (KajianVideo a in kajianVideo)
            {
                a.FImage = a.Source;
                context.DaftarKajianVideo.Add(a);
            }
            context.SaveChanges();

            /* 
             * Konsultasi E Paper 
             */
            var konsultasiEPaper = new List<KonsultasiEPaper>();

            foreach (PathKonsultasiEPaper s in pathKonsultasiEPaper)
            {
                string MyString = s.Path.ToString();
                int index = MyString.LastIndexOf(".");
                string JudulString = MyString.Replace(".pdf", "");
                JudulString = JudulString.Replace("/uploads/image/epaper/", "");
                konsultasiEPaper.Add(new KonsultasiEPaper{ Judul = JudulString, Tanggal = DateTime.Now, Path = s, Status = 1, Penulis = "admin", Source = s.Path });
            }

            foreach (KonsultasiEPaper a in konsultasiEPaper)
            {
                a.FImage = a.Source;
                context.DaftarKonsultasiEPaper.Add(a);
            }
            context.SaveChanges();

            /* 
             * Konsultasi Republika 
             */
            var konsultasiRepublika = new KonsultasiRepublika[]
            {
            new KonsultasiRepublika{Judul="Konsultasi Republika",Tanggal = DateTime.Now, Source = "", Status = 1,Penulis="admin"},
            new KonsultasiRepublika{Judul="Konsultasi Republika",Tanggal = DateTime.Now, Source = "", Status = 1,Penulis="admin"}
            };
            foreach (KonsultasiRepublika a in konsultasiRepublika)
            {
                context.DaftarKonsultasiRepublika.Add(a);
            }
            context.SaveChanges();

            /* 
             * Konsultasi Rumah Wasathia 
             */
            var konsultasiRumahWasathia = new KonsultasiRumahWasathia[]
            {
            new KonsultasiRumahWasathia{Judul="Konsultasi Rumah Wasathia",Tanggal=DateTime.Now,Pertanyaan="Lorem Ipsum is simply dummy text",Jawaban="Lorem Ipsum is simply dummy text", Status = 1,Penulis="admin"},
            new KonsultasiRumahWasathia{Judul="Konsultasi Rumah Wasathia",Tanggal=DateTime.Now,Pertanyaan="Lorem Ipsum is simply dummy text",Jawaban="Lorem Ipsum is simply dummy text", Status = 1,Penulis="admin"}
            };
            foreach (KonsultasiRumahWasathia a in konsultasiRumahWasathia)
            {
                context.DaftarKonsultasiRumahWasathia.Add(a);
            }
            context.SaveChanges();
        }
    }
}
