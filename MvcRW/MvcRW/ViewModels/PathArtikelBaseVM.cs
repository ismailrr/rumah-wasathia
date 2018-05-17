using Microsoft.AspNetCore.Mvc.Rendering;
using MvcRW.Data;
using MvcRW.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcRW.ViewModels
{
    public abstract class PathArtikelBaseVM
    {
        public PathArtikel PathArtikel { get; set; } = new PathArtikel();

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        public virtual void Init(Repository repository)
        {
        }
    }
}
