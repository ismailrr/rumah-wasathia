using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRWV2.Models.KajianVideoViewModels
{
    public class IndexKajianVideoViewModel
    {
        public PaginatedList<KajianVideo> KajianVideoModel { get; set; }
        public IEnumerable<Buku> BukuModel { get; set; }
        public IEnumerable<Artikel> ArtikelModel { get; set; }
    }
}
