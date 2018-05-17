using MvcRW.Data;
using MvcRW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcRW.ViewModels
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
