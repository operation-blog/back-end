using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GF.DAL.Entities;

namespace GC.BLL.Abstractions
{
    public interface IBlogService
    {
        Task<Blog> CreateBlog(int[] creators, string title);

        Task<IEnumerable<Blog>> GetAll();

        Task DeleteBlog(int blogID);

        Task<Blog> GetById(int id);
    }
}
