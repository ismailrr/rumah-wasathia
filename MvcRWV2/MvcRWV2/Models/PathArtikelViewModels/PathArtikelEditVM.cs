using MvcRWV2.Data;
using MvcRWV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcRWV2.Models.ViewModels
{
    public class PathArtikelEditVM
        : PathArtikelBaseVM
    {
        public int Id
        {
            get { return PathArtikel.Id; }
            set { PathArtikel.Id = value; }
        }
    }
}
