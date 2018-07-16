using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRWV2.Models.KonsultasiEPaperViewModels
{
    public class IndexKonsultasiEPaperViewModel : IEnumerable<IndexKonsultasiEPaperViewModel>
    {
        public PaginatedList<KonsultasiEPaper> KonsultasiEPaperModel { get; set; }
        public IEnumerable<Buku> BukuModel { get; set; }
        public IEnumerable<Artikel> ArtikelModel { get; set; }

        public IEnumerator<IndexKonsultasiEPaperViewModel> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
