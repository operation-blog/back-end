using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GC.BLL.Abstractions;
using GC.DTO.Responses;
using AutoMapper;
using GF.DAL.Entities;
using System.Linq;
using GC.DTO.Requests;

namespace GC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(BlogRequestDTO blogData)
        {
            var user = (User)HttpContext.Items["User"];

            if (blogData.Authors.Contains(user.ID))
                return BadRequest(new { status = 0, message = "Creator Can't be in Authors List" });

            var blog = await _blogService.CreateBlog(user, blogData.Authors, blogData.Title, blogData.Description, blogData.Data);

            if (blog == null)
                return BadRequest(new { status = 0, message = "Failed to Create Blog. Most Likely Authors Contained Invalid ID" });

            return Ok(new { status = 1, message = "Blog Created" });
        }


        [HttpGet("GetAll/{lastId?}")]
        public async Task<IEnumerable<BlogResponseDTO>> GetAll([FromRoute] int? lastId = null)
        {
            var blogs = await _blogService.GetAll(lastId.HasValue ? lastId.Value : 0);

            return _mapper.Map<List<BlogResponseDTO>>(blogs);
        }

        [HttpGet("Count")]
        public async Task<IActionResult> GetCount()
        {
            var count = await _blogService.GetBlogsCount();

            return Ok(new { status = 1, data = count });
        }

        [HttpGet("{blogId}")]
        public async Task<IActionResult> GetById([FromRoute] int blogId)
        {
            var blog = await _blogService.GetById(blogId);

            if (blog == null)
                return BadRequest(new { status = 0, message = "Blog Doesn't Exist" });

            return Ok(new { status = 1, data = _mapper.Map<BlogResponseDTO>(blog) });
        }

        [HttpGet("Details/{blogId}")]
        public async Task<IActionResult> GetDetails([FromRoute] int blogId)
        {
            var blog = await _blogService.GetById(blogId);

            if (blog == null)
                return BadRequest(new { status = 0, message = "Failed To Find Blog" });

            return Ok(new { status = 1, data = blog.Data });
        }

        [HttpGet("BlogsCount/{userId}")]
        public async Task<IActionResult> GetUserBlogsCount([FromRoute] int userId)
        {
            var blogCount = await _blogService.GetUserBlogCount(userId);

            if (blogCount == -1)
                return BadRequest(new { status = 0, message = "Invalid User" });

            return Ok(new { status = 1, data = blogCount });
        }

        [HttpGet("Blogs/{userId}")]
        public async Task<IActionResult> GetUserBlogs([FromRoute] int userId)
        {
            var blogs = await _blogService.GetUserBlogs(userId);

            if (blogs == null)
                return BadRequest(new { status = 0, message = "Invalid User" });

            return Ok(new { status = 1, data = _mapper.Map<List<BlogResponseDTO>>(blogs) });
        }

        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateBlog(int blogID, BlogUpdateRequestDTO blogUpdateData)
        {
            var user = (User)HttpContext.Items["User"];

            var blog = await _blogService.GetById(blogID);

            if (blog == null)
                return BadRequest(new { status = 0, message = "Blog Doesn't Exist" });

            if (!blog.Authors.Select(y => y.UserId).Contains(user.ID) && blog.OfficialCreator != user)
                return BadRequest(new { status = 0, message = "Current User Doesn't Own This Blog" });

            if (blogUpdateData.Authors.Contains(blog.OfficialCreator.ID))
                return BadRequest(new { status = 0, message = "Creator Can't be in Authors List" });

            if (await _blogService.UpdateBlog(blogID, user, blogUpdateData.Authors, blogUpdateData.Title, blogUpdateData.Description, blogUpdateData.Data))
                return Ok(new { status = 1, message = "Blog Updated" });
            else
                return BadRequest(new { status = 0, message = "Failed To Update Blog" });
        }

        [Authorize]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int blogID)
        {
            var user = (User)HttpContext.Items["User"];

            var blog = await _blogService.GetById(blogID);

            if (blog == null)
                return BadRequest(new { status = 0, message = "Blog Doesn't Exist" });

            if (blog.OfficialCreator != user)
                return BadRequest(new { status = 0, message = "Current User Doesn't Own This Blog" });

            await _blogService.DeleteBlog(blogID);

            return Ok(new { status = 1, message = "Blog Deleted" });
        }
    }
}
