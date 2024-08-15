using AutoMapper;
using EventWeb.API.DTOs;
using EventWeb.Core.Abstractions;
using EventWeb.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService; 
        private readonly IMapper _mapper; 

        public AccountController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper; 
            _userService = userService; 
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var token = await _userService.Login(request.Email, request.Password); 
            HttpContext.Response.Cookies.Append("NotJwtToken", token); 
            return Ok(token); 
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO request)
        {
            var user = _mapper.Map<User>(request); 
            await _userService.Register(user, request.Password); 
            return Ok(); 
        }
    }
}