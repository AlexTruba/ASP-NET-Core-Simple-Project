using CoreSimple.db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreSimple.Service
{
    public class ServiceAsync<T> where T: class
    {
        private readonly CoreContext _context;
        private readonly DbSet<T> _set;

        public ServiceAsync(CoreContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task AddAsync(T entry)
        {
            await _set.AddAsync(entry);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(T entry)
        {
            try
            {
                _set.Attach(entry);
                _context.Entry<T>(entry).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { }

        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.FirstOrDefaultAsync(predicate);
        }

        public async Task RemoveAsync(T entry)
        {
            if (entry != null)
            {
                _set.Remove(entry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
