using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRWV2.Models.KonsultasiRepublikaViewModels
{
    public class DetailsKonsultasiRepublikaViewModel : IEnumerable<DetailsKonsultasiRepublikaViewModel>
    {
        public KonsultasiRepublika KonsultasiRepublikaModel { get; set; }
        public IEnumerable<Buku> BukuModel { get; set; }
        public IEnumerable<Artikel> ArtikelModel { get; set; }

        public IEnumerator<DetailsKonsultasiRepublikaViewModel> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
