using MvcRW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcRW.Data
{
    public class PathArtikelRepository : BaseRepository<PathArtikel>
    {
        public PathArtikelRepository(RWContext context)
            : base(context)
        {
        }

        public override PathArtikel Get(int id, bool includeRelatedEntities = true)
        {
            var pathArtikel = RWContext.DaftarPathArtikel.AsQueryable();

            return pathArtikel
                .Where(cb => cb.Id == id)
                .SingleOrDefault();
        }

        public override IList<PathArtikel> GetList()
        {
            return RWContext.DaftarPathArtikel
                .OrderBy(cb => cb.Tanggal)
                .ToList();
        }
    }
}
