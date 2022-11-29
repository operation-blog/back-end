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
        Task<Blog> CreateBlog(User officialCreator, int[] creators, string title, string description, string data);

        Task<IEnumerable<Blog>> GetAll(int lastId = 0);

        Task<int> GetBlogsCount();

        Task DeleteBlog(int blogID);

        Task<bool> UpdateBlog(int blogID, User officialCreator, int[] creators, string title, string description, string data);

        Task<Blog> GetById(int id);

        Task<int> GetUserBlogCount(int userID);

        Task<List<Blog>> GetUserBlogs(int userID);
    }
}
