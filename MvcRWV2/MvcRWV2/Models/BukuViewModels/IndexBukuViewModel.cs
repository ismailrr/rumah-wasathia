using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRWV2.Models.BukuViewModels
{
    public class IndexBukuViewModel : IEnumerable<IndexBukuViewModel>
    {
        public IEnumerable<Artikel> ArtikelModel { get; set; }
        public PaginatedList<Buku> BukuModel { get; set; }
        public IEnumerable<KonsultasiRepublika> KonsultasiRepublikaModel { get; set; }

        public IEnumerator<IndexBukuViewModel> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
