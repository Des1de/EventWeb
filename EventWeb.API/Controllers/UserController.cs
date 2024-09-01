using AutoMapper;
using EventWeb.API.DTOs;
using EventWeb.Application.UseCases;
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
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _getAllUsersUseCase.GetAllUsers();
            var response = _mapper.Map<IEnumerable<UserResponseDTO>>(users); 
            return Ok(response);
        }
    }
}