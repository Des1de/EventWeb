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
    public class ParticipationController : ControllerBase
    {
        private readonly IParticipationService _participationService;
        private readonly IMapper _mapper;
        public ParticipationController(IParticipationService participationService, IMapper mapper)
        {
            _mapper = mapper; 
            _participationService = participationService; 
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetParticipationsByEventId(Guid id)
        {
            var events = await _participationService.GetParticipationsByEventId(id);
            var response = _mapper.Map<IEnumerable<ParticipationResponseDTO>>(events); 
            return Ok(response); 
        }

        [HttpPost]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> CreateParticipation([FromBody] ParticipationRequestDTO request)
        {
            var participation = _mapper.Map<Participation>(request); 
            await _participationService.CreateParticipation(participation); 
            return NoContent(); 
        }
        [HttpDelete]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> DeleteParticipation([FromBody] Guid id)
        {
            await _participationService.DeleteParticipation(id); 
            return NoContent(); 
        }
    }
}