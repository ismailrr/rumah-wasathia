using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRWV2.Models.GaleriViewModels
{
    public class IndexGaleriViewModel : IEnumerable<IndexGaleriViewModel>
    {
        public PaginatedList<Galeri> GaleriModel { get; set; }
        public IEnumerable<Buku> BukuModel { get; set; }
        public IEnumerable<Artikel> ArtikelModel { get; set; }

        public IEnumerator<IndexGaleriViewModel> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
