using MvcRWV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcRWV2.Data
{
    public class Repository
    {
        private ApplicationDbContext _context = null;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
