using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRWV2.Models.KonsultasiRumahWasathiaViewModels
{
    public class IndexKonsultasiRWViewModel : IEnumerable<IndexKonsultasiRWViewModel>
    {
        public PaginatedList<KonsultasiRumahWasathia> KonsultasiRumahWasathiaModel { get; set; }
        public IEnumerable<Buku> BukuModel { get; set; }
        public IEnumerable<Artikel> ArtikelModel { get; set; }

        public IEnumerator<IndexKonsultasiRWViewModel> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
