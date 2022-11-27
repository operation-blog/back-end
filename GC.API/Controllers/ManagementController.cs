using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GC.BLL.Abstractions;
using GC.DTO.Responses;
using AutoMapper;
using GF.DAL.Entities;

namespace GC.API.Controllers
{
    [Authorize(Role.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly IAccessTokenService _tokenService;
        private readonly IMapper _mapper;

        public ManagementController(IAccessTokenService tokenService, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpGet("Token/Create")]
        public async Task<TokenResponseDTO> Create()
        {
            var user = (User)HttpContext.Items["User"];

            var token = await _tokenService.CreateToken(user);

            return _mapper.Map<TokenResponseDTO>(token);
        }


        [HttpGet("Token/GetAll/{lastId?}")]
        public async Task<IEnumerable<TokenResponseDTO>> GetAll([FromRoute] int? lastId = null)
        {
            var tokens = await _tokenService.GetAll(lastId.HasValue ? lastId.Value : 0);

            return _mapper.Map<List<TokenResponseDTO>>(tokens);
        }
    }
}
