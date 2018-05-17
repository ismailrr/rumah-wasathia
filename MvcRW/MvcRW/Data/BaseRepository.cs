using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcRW.Data
{
    public abstract class BaseRepository<TEntity>
        where TEntity : class
    {
        protected RWContext RWContext { get; private set; }

        public BaseRepository(RWContext context)
        {
            RWContext = context;
        }

        public abstract TEntity Get(int id, bool includeRelatedEntities = true);
        public abstract IList<TEntity> GetList();   

        public void Add(TEntity entity)
        {
            RWContext.Set<TEntity>().Add(entity);
            RWContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            RWContext.Entry(entity).State = EntityState.Modified;
            RWContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var set = RWContext.Set<TEntity>();
            var entity = set.Find(id);
            set.Remove(entity);
            RWContext.SaveChanges();
        }
    }
}
