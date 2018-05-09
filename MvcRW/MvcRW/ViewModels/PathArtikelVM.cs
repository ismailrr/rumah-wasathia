using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcRW.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcRW.ViewModels
{
    public class PathArtikelVM
    {
        public PathArtikel PathArtikel { get; set; }
        public IFormFile Path { get; set; }

        public PathArtikelVM()
        {
            
        }

        public PathArtikelVM(PathArtikel pathArtikel)
        {
            PathArtikel = pathArtikel;
        }
    }
}
