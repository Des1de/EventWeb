using AutoMapper;
using EventWeb.API.DTOs;
using EventWeb.Application.Abstractions;
using EventWeb.Application.UseCases;
using EventWeb.Core.Abstractions;
using EventWeb.Core.Models;
using EventWeb.Core.Models.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly CreateEventUseCase _createEventUseCase; 
        private readonly UpdateEventUseCase _updateEventUseCase;
        private readonly DeleteEventUseCase _deleteEventUseCase; 
        private readonly GetAllEventsUseCase _getAllEventsUseCase; 
        private readonly GetEventByIdUseCase _getEventByIdUseCase; 
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EventController(IMapper mapper, 
            IFileService fileService, 
            IWebHostEnvironment hostingEnvironment,
            CreateEventUseCase createEventUseCase,
            UpdateEventUseCase updateEventUseCase, 
            DeleteEventUseCase deleteEventUseCase, 
            GetAllEventsUseCase getAllEventsUseCase, 
            GetEventByIdUseCase getEventByIdUseCase)
        {  
            _mapper = mapper;
            _fileService = fileService; 
            _hostingEnvironment = hostingEnvironment; 
            _createEventUseCase = createEventUseCase; 
            _updateEventUseCase = updateEventUseCase; 
            _deleteEventUseCase = deleteEventUseCase; 
            _getAllEventsUseCase = getAllEventsUseCase;
            _getEventByIdUseCase = getEventByIdUseCase; 
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] EventParameters parameters)
        {
            var events = await _getAllEventsUseCase.GetAllEvents(parameters);
            var eventDtos = new List<EventResponseDTO>();

            foreach (var eventEntity in events)
            {
                var eventDto = _mapper.Map<EventResponseDTO>(eventEntity);
                if (eventEntity.ImageUrl != null) 
                {
                    var imageBytes = await _fileService.LoadFileAsync(eventEntity.ImageUrl);
                    eventDto.ImageBase64 = Convert.ToBase64String(imageBytes);
                }

                eventDtos.Add(eventDto);
            }

            return Ok(eventDtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            var eventEntity = await _getEventByIdUseCase.GetEventById(id); 
            var eventDto = _mapper.Map<EventResponseDTO>(eventEntity);
            if(eventEntity?.ImageUrl != null) 
            {
                var imageBytes = await _fileService.LoadFileAsync(eventEntity.ImageUrl);
                eventDto.ImageBase64 = Convert.ToBase64String(imageBytes);
            }
            return Ok(eventDto);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateEvent([FromForm] EventRequestDTO request)
        {
            var newEvent = _mapper.Map<Event>(request);
            newEvent.ImageUrl = await _fileService.SaveFileAsync(request.Image, _hostingEnvironment.ContentRootPath + "/content");
            await _createEventUseCase.CreateEvent(newEvent);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromForm] EventRequestDTO request)
        {
            var oldUrl = (await _getEventByIdUseCase.GetEventById(id))?.ImageUrl;
            var updatedEvent = _mapper.Map<Event>(request);
            _fileService.DeleteFile(oldUrl);
            updatedEvent.ImageUrl = await _fileService.SaveFileAsync(request.Image, _hostingEnvironment.ContentRootPath + "/content");
            await _updateEventUseCase.UpdateEvent(id, updatedEvent);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var deletingEvent = await _getEventByIdUseCase.GetEventById(id); 
            var ImageUrl = deletingEvent?.ImageUrl; 
            _fileService.DeleteFile(ImageUrl);
            await _deleteEventUseCase.DeleteEvent(id);
            return NoContent();
        }
    }
}