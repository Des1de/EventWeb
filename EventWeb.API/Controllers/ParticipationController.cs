using AutoMapper;
using EventWeb.API.DTOs;
using EventWeb.Application.UseCases;
using EventWeb.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipationController : ControllerBase
    {
        private readonly GetParticipationsByEventIdUseCase _getParticipationsByEventIdUseCase;
        private readonly CreateParticipationUseCase _createParticipationUseCase;
        private readonly DeleteParticipationUseCase _deleteParticipationUseCase; 
        private readonly IMapper _mapper;
        public ParticipationController(IMapper mapper,
            GetParticipationsByEventIdUseCase getParticipationsByEventIdUseCase,
            CreateParticipationUseCase createParticipationUseCase,
            DeleteParticipationUseCase deleteParticipationUseCase)
        {
            _mapper = mapper; 
            _getParticipationsByEventIdUseCase = getParticipationsByEventIdUseCase; 
            _createParticipationUseCase = createParticipationUseCase; 
            _deleteParticipationUseCase = deleteParticipationUseCase; 
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetParticipationsByEventId(Guid id)
        {
            var events = await _getParticipationsByEventIdUseCase.GetParticipationsByEventId(id);
            var response = _mapper.Map<IEnumerable<ParticipationResponseDTO>>(events); 
            return Ok(response); 
        }

        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> CreateParticipation([FromBody] ParticipationRequestDTO request)
        {
            var participation = _mapper.Map<Participation>(request); 
            await _createParticipationUseCase.CreateParticipation(participation); 
            return NoContent(); 
        }
        [HttpDelete]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> DeleteParticipation([FromBody] Guid id)
        {
            await _deleteParticipationUseCase.DeleteParticipation(id); 
            return NoContent(); 
        }
    }
}