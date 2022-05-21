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
    [Authorize]
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
        
        [HttpPost("Create")]
        public async Task<IActionResult> Create(BlogRequestDTO blogData)
        {
            var user = (User)HttpContext.Items["User"];

            if (!blogData.Authors.Contains(user.ID))
                return BadRequest(new { status = 0, message = "Authors Doesn't Contain Current User" });

            var blog = await _blogService.CreateBlog(blogData.Authors, blogData.Title, blogData.Data);

            if (blog == null)
                return BadRequest(new { status = 0, message = "Failed to Create Blog. Most Likely Authors Contained Invalid ID" });

            return Ok(new { status = 1, message = "Blog Created" });
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<BlogResponseDTO>> GetAll()
        {
            var blogs = await _blogService.GetAll();

            return _mapper.Map<List<BlogResponseDTO>>(blogs);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int blogID)
        {
            var user = (User)HttpContext.Items["User"];

            var blog = await _blogService.GetById(blogID);

            if (blog == null)
                return BadRequest(new { status = 0, message = "Blog Doesn't Exist" });

            if (!blog.Authors.Select(y => y.UserId).Contains(user.ID))
                return BadRequest(new { status = 0, message = "Current User Doesn't Own This Blog" });

            await _blogService.DeleteBlog(blogID);

            return Ok(new { status = 1, message = "Blog Deleted" });
        }
    }
}
