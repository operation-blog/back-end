using GF.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GF.DAL.Abstractions
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll(int lastId = 0);

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
