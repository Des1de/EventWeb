using AutoMapper;
using EventWeb.API.DTOs;
using EventWeb.Core.Abstractions;
using EventWeb.Core.Models;
using EventWeb.Core.Models.Parameters;
using EventWeb.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EventController(IEventService eventService, IMapper mapper, IFileService fileService, IWebHostEnvironment hostingEnvironment)
        {
            _eventService = eventService;   
            _mapper = mapper;
            _fileService = fileService; 
            _hostingEnvironment = hostingEnvironment; 
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] EventParameters parameters)
        {
            var events = await _eventService.GetAllEvents(parameters);
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
            var eventEntity = await _eventService.GetEventById(id); 
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
            await _eventService.CreateEvent(newEvent);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromForm] EventRequestDTO request)
        {
            var oldUrl = (await _eventService.GetEventById(id))?.ImageUrl;
            var updatedEvent = _mapper.Map<Event>(request);
            _fileService.DeleteFile(oldUrl);
            updatedEvent.ImageUrl = await _fileService.SaveFileAsync(request.Image, _hostingEnvironment.ContentRootPath + "/content");
            await _eventService.UpdateEvent(id, updatedEvent);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var deletingEvent = await _eventService.GetEventById(id); 
            var ImageUrl = deletingEvent?.ImageUrl; 
            _fileService.DeleteFile(ImageUrl);
            await _eventService.DeleteEvent(id);
            return NoContent();
        }
    }
}