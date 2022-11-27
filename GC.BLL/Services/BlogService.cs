using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GC.BLL.Abstractions;
using GF.DAL.Abstractions;
using GF.DAL.Entities;

namespace GC.BLL.Services
{
    public class BlogService : IBlogService
    {
        private IGenericRepository<Blog> _blogRepository = null;
        private IGenericRepository<User> _userRepository = null;

        public BlogService(IGenericRepository<Blog> blogRepository, IGenericRepository<User> userRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }

        private async Task<bool> UpdateBlogAuthors(Blog blog, int[] authors)
        {
            blog.Authors = new List<BlogUser>();

            foreach (var author in authors)
            {
                var userToAdd = await _userRepository.GetById(author);

                if (userToAdd == null)
                    return false;

                var newAuthor = new BlogUser
                {
                    User = userToAdd,
                    Blog = blog
                };

                blog.Authors.Add(newAuthor);
            }

            return true;
        }

        public async Task<int> GetUserBlogCount(int userID)
        {
            var user = await _userRepository.GetById(userID);

            if (user == null)
                return -1;

           return await _blogRepository.GetCountFromQueryable(_blogRepository.GetQueryable().Where(e => e.OfficialCreator.ID == user.ID));
        }

        public async Task<List<Blog>> GetUserBlogs(int userID)
        {
            var user = await _userRepository.GetById(userID);

            if (user == null)
                return null;

            return await _blogRepository.GetAllFromQueryable(_blogRepository.GetQueryable().Where(e => e.OfficialCreator.ID == user.ID));
        }

        public async Task<Blog> GetById(int id)
        {
            return await _blogRepository.GetById(id);
        }

        public async Task<Blog> CreateBlog(User officialCreator, int[] creators, string title, string description, string data)
        {
            Blog blog = new Blog();

            blog.OfficialCreator = officialCreator;
            blog.Title = title;
            blog.Description = description;
            blog.Data = data;

            if (!await UpdateBlogAuthors(blog, creators))
                return null;

            _blogRepository.Insert(blog);
            await _blogRepository.Save();

            return blog;
        }

        public async Task<bool> UpdateBlog(int blogID, User officialCreator, int[] creators, string title, string description, string data)
        {
            var blog = await GetById(blogID);

            blog.OfficialCreator = officialCreator;
            blog.Title = title;
            blog.Description = description;
            blog.Data = data;

            if (!await UpdateBlogAuthors(blog, creators))
                return false;

            _blogRepository.Update(blog);
            await _blogRepository.Save();

            return true;
        }

        public async Task<IEnumerable<Blog>> GetAll(int lastId)
        {
            return await _blogRepository.GetAll(lastId);
        }

        public async Task DeleteBlog(int blogID)
        {
            var blog = await GetById(blogID);

            _blogRepository.Delete(blog);

            await _blogRepository.Save();
        }
    }
}
