using Microsoft.AspNetCore.Mvc.Rendering;
using MvcRWV2.Data;
using MvcRWV2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcRWV2.Models.ViewModels
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
