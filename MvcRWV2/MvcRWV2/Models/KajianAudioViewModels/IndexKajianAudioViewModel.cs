using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRWV2.Models.KajianAudioViewModels
{
    public class IndexKajianAudioViewModel
    {
        public PaginatedList<KajianAudio> KajianAudioModel { get; set; }
        public IEnumerable<Buku> BukuModel { get; set; }
        public IEnumerable<Artikel> ArtikelModel { get; set; }
    }
}
