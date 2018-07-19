using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRWV2.Models.GaleriViewModels
{
    public class DetailsGaleriViewModel : IEnumerable<DetailsGaleriViewModel>
    {
        public Galeri GaleriModel { get; set; }
        public IEnumerable<Buku> BukuModel { get; set; }
        public IEnumerable<Artikel> ArtikelModel { get; set; }
        public string[] SourceImage { get; set; }

        public IEnumerator<DetailsGaleriViewModel> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
