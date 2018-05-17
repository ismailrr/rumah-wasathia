using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcRW.Data;

namespace MvcRW.Controllers
{
    public abstract class BaseController : Controller
    {
        private bool _disposed = false;

        protected RWContext RWContext { get; private set; }
        protected Repository Repository { get; private set; }

        public BaseController(RWContext context)
        {
            RWContext = context;
            Repository = new Repository(RWContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                RWContext.Dispose();
            }

            _disposed = true;

            base.Dispose(disposing);
        }
    }
}