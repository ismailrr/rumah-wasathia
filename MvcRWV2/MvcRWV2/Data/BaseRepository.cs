using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcRWV2.Data
{
    public abstract class BaseRepository<TEntity>
        where TEntity : class
    {
        protected ApplicationDbContext ApplicationDbContext { get; private set; }

        public BaseRepository(ApplicationDbContext context)
        {
            ApplicationDbContext = context;
        }

        public abstract TEntity Get(int id, bool includeRelatedEntities = true);
        public abstract IList<TEntity> GetList();   

        public void Add(TEntity entity)
        {
            ApplicationDbContext.Set<TEntity>().Add(entity);
            ApplicationDbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            ApplicationDbContext.Entry(entity).State = EntityState.Modified;
            ApplicationDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var set = ApplicationDbContext.Set<TEntity>();
            var entity = set.Find(id);
            set.Remove(entity);
            ApplicationDbContext.SaveChanges();
        }
    }
}
