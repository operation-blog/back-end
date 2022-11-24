using GF.DAL.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GF.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DatabaseContext _context = null;

        private DbSet<T> table = null;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public async Task<int> GetCount()
        {
            return await table.CountAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return table.Where(predicate);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(T obj)
        {
            _context.Entry(obj).State = EntityState.Deleted;
            table.Remove(obj);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
