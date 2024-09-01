using System.Linq;
using AutoMapper;
using EventWeb.API.DTOs;
using EventWeb.Application.UseCases;
using EventWeb.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly RegisterUserUseCase _registerUserUseCase;
        private readonly LoginUserUseCase _loginUserUseCase;
        private readonly RefreshUserUseCase _refreshUserUseCase;
        private readonly IMapper _mapper; 

        public AccountController(IMapper mapper, 
            RegisterUserUseCase registerUserUseCase,
            LoginUserUseCase loginUserUseCase,
            RefreshUserUseCase refreshUserUseCase)
        {
            _mapper = mapper; 
            _registerUserUseCase = registerUserUseCase; 
            _loginUserUseCase = loginUserUseCase;
            _refreshUserUseCase = refreshUserUseCase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequestDTO request)
        {
            var tokens = await _loginUserUseCase.Login(request.Email, request.Password); 
            HttpContext.Response.Cookies.Append("NotJwtToken", tokens.AccessToken);
            HttpContext.Response.Cookies.Append("NotRefreshToken", tokens.RefreshToken); 
            return Ok(tokens.AccessToken); 
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDTO request)
        {
            var refreshToken = HttpContext.Request.Cookies["NotRefreshToken"]; 
            if(refreshToken is null)
            {
                return BadRequest(); 
            }
            var tokens = await _refreshUserUseCase.Refresh(request.Email, refreshToken); 
            HttpContext.Response.Cookies.Append("NotJwtToken", tokens.AccessToken);
            HttpContext.Response.Cookies.Append("NotRefreshToken", tokens.RefreshToken); 
            return Ok(tokens.AccessToken); 
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegistrationRequestDTO request)
        {
            var user = _mapper.Map<User>(request); 
            await _registerUserUseCase.Register(user, request.Password); 
            return Ok(); 
        }
    }
}