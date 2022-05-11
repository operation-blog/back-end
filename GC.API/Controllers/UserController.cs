using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GC.BLL.Abstractions;
using GC.DTO.Responses;
using AutoMapper;

namespace GC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("Ping")]
        public string Ping() { return "OK"; }

        [HttpGet]
        public async Task<IEnumerable<UserResponseDTO>> GetAll()
        {
            var userResponse = await _userService.GetAll();

            return _mapper.Map<List<UserResponseDTO>>(userResponse);
        }
    }
}
