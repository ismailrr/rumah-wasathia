using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRWV2.Models.ArtikelViewModels
{
    public class IndexArtikelViewModel : IEnumerable<IndexArtikelViewModel>
    {
        public PaginatedList<Artikel> ArtikelModel { get; set; }
        public IEnumerable<Buku> BukuModel { get; set; }
        public IEnumerable<KonsultasiRepublika> KonsultasiRepublikaModel { get; set; }

        public IEnumerator<IndexArtikelViewModel> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
