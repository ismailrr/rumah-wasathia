using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcRWV2.Data;

namespace MvcRWV2.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public abstract class BaseController : Controller
    {
        private bool _disposed = false;

        protected ApplicationDbContext ApplicationDbContext { get; private set; }
        protected Repository Repository { get; private set; }

        public BaseController(ApplicationDbContext context)
        {
            ApplicationDbContext = context;
            Repository = new Repository(ApplicationDbContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                ApplicationDbContext.Dispose();
            }

            _disposed = true;

            base.Dispose(disposing);
        }
    }
}