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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService; 
        private readonly IMapper _mapper; 

        public UserController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper; 
            _userService = userService; 
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            var response = _mapper.Map<IEnumerable<UserResponseDTO>>(users); 
            return Ok(response);
        }
    }
}