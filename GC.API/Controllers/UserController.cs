using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GC.BLL.Abstractions;
using GC.DTO.Responses;
using AutoMapper;
using GC.DTO.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using GF.DAL.Entities;

namespace GC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAccessTokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IAccessTokenService tokenService,  IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet("Ping")]
        public string Ping() { return "OK"; }

        [Authorize(Role.Admin)]
        [HttpGet("All")]
        public async Task<IEnumerable<UserResponseDTO>> GetAll()
        {
            var userResponse = await _userService.GetAll();

            return _mapper.Map<List<UserResponseDTO>>(userResponse);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            var user = (User)HttpContext.Items["User"];

            return Ok(new { status = 1, data = _mapper.Map<UserResponseDTO>(user) });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserRequestDTO loginData)
        {
            var user = await _userService.GetUserByDetails(loginData.Username, loginData.Password);

            if (user == null)
                return BadRequest(new { status = 0, message = "Username or Password is Incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()), new Claim("role", user.Role.ToString()) }),

                Expires = DateTime.UtcNow.AddDays(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { status = 1, username = loginData.Username, role = user.Role, token = tokenHandler.WriteToken(token) });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestDTO registerData)
        {
            var token = await _tokenService.TokenExist(registerData.Token);

            if (token == null)
                return BadRequest(new { status = 0, message = "Invalid Token" });

            var user = await _userService.CreateUser(registerData.Username, registerData.Password);

            if (user == null)
                return BadRequest(new { status = 0, message = "Name Already Exist" });

            await _tokenService.MarkTokenUsed(token, user);

            return Ok(new { status = 1, message = "Account Created" });
        }
    }
}
