using AutoMapper;
using EventWeb.API.DTOs;
using EventWeb.Application.UseCases;
using EventWeb.Core.Abstractions;
using EventWeb.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly GetAllUsersUseCase _getAllUsersUseCase;  
        private readonly IMapper _mapper; 

        public UserController(IMapper mapper,
            GetAllUsersUseCase getAllUsersUseCase)
        {
            _mapper = mapper; 
            _getAllUsersUseCase = getAllUsersUseCase; 
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _getAllUsersUseCase.GetAllUsers();
            var response = _mapper.Map<IEnumerable<UserResponseDTO>>(users); 
            return Ok(response);
        }
    }
}