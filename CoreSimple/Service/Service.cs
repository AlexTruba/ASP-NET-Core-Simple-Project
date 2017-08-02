using CoreSimple.db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimple.Service
{
    public class Service<T> : IService<T> where T:class
    {
        private readonly CoreContext _context;
        private readonly DbSet<T> _set;

        public Service(CoreContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            return _context.Set<T>().ToList();
        }
        public void Add(T entry)
        {
            _set.Add(entry);
            _context.SaveChanges();
        }

        public void Edit(T entry)
        {
            try
            {
                _set.Attach(entry);
                _context.Entry<T>(entry).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e) { }

        }

        public T Get(Func<T, bool> predicate)
        {
            return _set.FirstOrDefault(predicate);
        }

        public void Remove(T entry)
        {
            if (entry != null)
            {
                _set.Remove(entry);
                _context.SaveChanges();
            }
        }
    }
}
