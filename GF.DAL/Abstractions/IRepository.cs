using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GF.DAL.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(object id);

        Task<int> GetCountFromQueryable(IQueryable<T> queryable);

        Task<T> GetFirstFromQueryable(IQueryable<T> queryable);

        Task<List<T>> GetAllFromQueryable(IQueryable<T> queryable);

        IQueryable<T> GetQueryable();

        void Insert(T obj);

        void Update(T obj);

        void Delete(T obj);

        Task Save();
    }
}
