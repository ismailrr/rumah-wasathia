using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcRW.Data;
using MvcRW.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace MvcRW.ViewModels
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
