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
        private readonly IMapper _mapper; 

        public AccountController(IMapper mapper, 
            RegisterUserUseCase registerUserUseCase,
            LoginUserUseCase loginUserUseCase)
        {
            _mapper = mapper; 
            _registerUserUseCase = registerUserUseCase; 
            _loginUserUseCase = loginUserUseCase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequestDTO request)
        {
            var token = await _loginUserUseCase.Login(request.Email, request.Password); 
            HttpContext.Response.Cookies.Append("NotJwtToken", token); 
            return Ok(token); 
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