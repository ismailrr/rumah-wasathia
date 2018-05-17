using MvcRW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcRW.Data
{
    public class Repository
    {
        private RWContext _context = null;

        public Repository(RWContext context)
        {
            _context = context;
        }
    }
}
