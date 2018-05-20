using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcRWV2.Data;
using MvcRWV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace MvcRWV2.ViewModels
{
    public class PathArtikelAddVM : PathArtikelBaseVM
    {

        public IFormFile Path { get; set; }

        public PathArtikelAddVM()
        {
            PathArtikel.Tanggal = DateTime.Now;
            
        }

        public override void Init(Repository repository)
        {
            base.Init(repository);
        }
    }
}
