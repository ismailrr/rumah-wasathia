using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRWV2.Models.rumahwasathiaViewModels
{
    public class IndexViewModel : IEnumerable<IndexViewModel>
    {
        public IEnumerable<Artikel> ArtikelModel { get; set; }
        public IEnumerable<Buku> BukuModel { get; set; }
        public IEnumerable<Galeri> GaleriModel { get; set; }
        public IEnumerable<KajianAudio> KajianAudioModel { get; set; }
        public IEnumerable<KajianVideo> KajianVideoModel { get; set; }

        public IEnumerator<IndexViewModel> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
