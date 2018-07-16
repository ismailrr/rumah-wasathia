using Microsoft.EntityFrameworkCore;
using MvcRWV2.Data;
using MvcRWV2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRWV2
{
    public class SideBar<T> : IEnumerable<T>
    {
        private readonly ApplicationDbContext _context;
        public PaginatedList<T> PaginationModel { get; private set; }
        public IEnumerable<Artikel> ArtikelModel { get; private set; }
        public IEnumerable<Buku> BukuModel { get; private set; }
        public IEnumerable<KonsultasiRepublika> KonsultasiRepublikaModel { get; set; }
        private int published = 1;
        private int trash = 2;

        public SideBar(ApplicationDbContext context)
        {
            _context = context;
        }
        public SideBar(IEnumerable<Artikel> artikel, IEnumerable<Buku> buku, IEnumerable<KonsultasiRepublika> konsultasiRepublika, PaginatedList<T> pagination)
        {
            ArtikelModel = artikel;
            BukuModel = buku;
            KonsultasiRepublikaModel = konsultasiRepublika;
            PaginationModel = pagination; 
        }

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var BukuModel = from s in _context.DaftarBuku
                       .Include(ee => ee.Kategori)
                       .Include(ee => ee.Path)
                       .Take(4)
                        where s.Status == published
                        select s;
            var KonsultasiRepublikaModel = from s in _context.DaftarKonsultasiRepublika
                       .Include(ee => ee.Kategori)
                       .Take(4)
                       where s.Status == published
                       select s;
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
