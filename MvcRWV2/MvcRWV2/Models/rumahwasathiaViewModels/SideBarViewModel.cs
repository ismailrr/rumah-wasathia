using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRWV2.Models.rumahwasathiaViewModels
{
    public class SideBarViewModel : IEnumerable<SideBarViewModel>
    {
        public IEnumerable<Artikel> ArtikelModel { get; set; }
        public IEnumerable<Buku> BukuModel { get; set; }
        public IEnumerable<KonsultasiRepublika> KonsultasiRepublikaModel { get; set; }

        public IEnumerator<SideBarViewModel> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
