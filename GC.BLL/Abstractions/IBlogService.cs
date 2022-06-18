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
        Task<Blog> CreateBlog(int[] creators, string title, string data);

        Task<IEnumerable<Blog>> GetAll();

        Task DeleteBlog(int blogID);

        Task<bool> UpdateBlog(int blogID, int[] creators, string title, string data);

        Task<Blog> GetById(int id);
    }
}
