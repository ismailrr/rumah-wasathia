using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRWV2.Models.KonsultasiInfografisViewModels
{
    public class IndexKonsultasiInfografisViewModel : IEnumerable<IndexKonsultasiInfografisViewModel>
    {
        public IEnumerable<Artikel> ArtikelModel { get; set; }
        public IEnumerable<Buku> BukuModel { get; set; }
        public PaginatedList<KonsultasiInfografis> KonsultasiInfografisModel { get; set; }

        public IEnumerator<IndexKonsultasiInfografisViewModel> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
