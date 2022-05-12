﻿using System;
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

        public async Task<Blog> CreateBlog(int[] creators, string title)
        {
            Blog blog = new Blog();

            blog.Title = title;

            foreach (var creator in creators)
            {
                var userToAdd = await _userRepository.GetById(creator);

                if (userToAdd == null)
                    return null;

                var newAuthor = new BlogUser
                {
                    User = userToAdd,
                    Blog = blog
                };

                blog.Authors.Add(newAuthor);
            }

            _blogRepository.Insert(blog);
            await _blogRepository.Save();

            return blog;
        }

        public async Task<IEnumerable<Blog>> GetAll()
        {
            return await _blogRepository.GetAll();
        }
    }
}