using GF.DAL.Abstractions;
using GF.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GF.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private DatabaseContext _context = null;

        private DbSet<T> table = null;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public async Task<int> GetCountFromQueryable(IQueryable<T> queryable)
        {
            return await queryable.CountAsync();
        }

        public async Task<T> GetFirstFromQueryable(IQueryable<T> queryable)
        {
            return await queryable.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllFromQueryable(IQueryable<T> queryable)
        {
            return await queryable.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(int lastId)
        {
            return await table.OrderBy(e => e.ID).Where(e => e.ID > lastId).Take(12).ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }

        public IQueryable<T> GetQueryable()
        {
            return table.AsQueryable<T>();
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
