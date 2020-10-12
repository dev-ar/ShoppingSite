using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;

namespace Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ShopSiteDB _context;
        private DbSet<T> _table;

        public GenericRepository(ShopSiteDB db)
        {
            _context = db;
            _table = db.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _table.ToList();

        }

        public virtual T GetById(object id)
        {
            return _table.Find(id);
        }

        public virtual void Insert(T obj)
        {
            _table.Add(obj);
        }

        public virtual void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public virtual void Delete(T obj)
        {
            if (_context.Entry(obj).State == EntityState.Detached)
            {
                _table.Attach(obj);
            }

            _table.Remove(obj);
        }

        public virtual void Delete(object id)
        {
            Delete(GetById(id));
        }
    }
}
