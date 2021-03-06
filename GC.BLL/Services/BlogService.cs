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

        public async Task<Blog> GetById(int id)
        {
            return await _blogRepository.GetById(id);
        }

        public async Task<Blog> CreateBlog(int[] creators, string title, string data)
        {
            Blog blog = new Blog();

            blog.Title = title;
            blog.Data = data;

            if (!await UpdateBlogAuthors(blog, creators))
                return null;

            _blogRepository.Insert(blog);
            await _blogRepository.Save();

            return blog;
        }

        public async Task<bool> UpdateBlog(int blogID, int[] creators, string title, string data)
        {
            var blog = await GetById(blogID);

            blog.Title = title;
            blog.Data = data;

            if (!await UpdateBlogAuthors(blog, creators))
                return false;

            _blogRepository.Update(blog);
            await _blogRepository.Save();

            return true;
        }

        public async Task<IEnumerable<Blog>> GetAll()
        {
            return await _blogRepository.GetAll();
        }

        public async Task DeleteBlog(int blogID)
        {
            var blog = await GetById(blogID);

            _blogRepository.Delete(blog);

            await _blogRepository.Save();
        }
    }
}
