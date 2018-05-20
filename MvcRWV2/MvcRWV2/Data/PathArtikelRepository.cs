using MvcRWV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcRWV2.Data
{
    public class PathArtikelRepository : BaseRepository<PathArtikel>
    {
        public PathArtikelRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override PathArtikel Get(int id, bool includeRelatedEntities = true)
        {
            var pathArtikel = ApplicationDbContext.DaftarPathArtikel.AsQueryable();

            return pathArtikel
                .Where(cb => cb.Id == id)
                .SingleOrDefault();
        }

        public override IList<PathArtikel> GetList()
        {
            return ApplicationDbContext.DaftarPathArtikel
                .OrderBy(cb => cb.Tanggal)
                .ToList();
        }
    }
}
