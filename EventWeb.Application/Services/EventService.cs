using EventWeb.Application.Exceptions;
using EventWeb.Core.Abstractions;
using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;
using EventWeb.Core.Models.Parameters;
using Microsoft.Extensions.Logging;

namespace EventWeb.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly ILogger<EventService> _logger;

        public EventService(IUnitOfWork unitOfWork, ILogger<EventService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork; 
        }

        public async Task<IEnumerable<Event>> GetAllEvents(EventParameters parameters)
        {
            return await _unitOfWork.EventRepository.GetEventsPaging( e => true, 
            parameters);
        }

        public async Task<Event?> GetEventById(Guid eventId)
        {
            return await _unitOfWork.EventRepository.GetSingleAsync(e => e.Id == eventId);
        }

        public async Task<Event?> GetEventByName(string name)
        {
            return await _unitOfWork.EventRepository.GetSingleAsync(e => e.Name == name);
        }

        public async Task CreateEvent(Event newEvent)
        {
            _unitOfWork.EventRepository.Create(newEvent);
            await _unitOfWork.SaveChangesAsync(); 
        }

        public async Task UpdateEvent(Guid eventId, Event updatedEvent)
        {
            var originEvent = await _unitOfWork.EventRepository.GetSingleAsync(e => e.Id == eventId); 
            if(originEvent == null)
            {
                throw new NotFoundException($"Event with id {eventId} not found"); 
            }
            updatedEvent.Id = eventId; 
            _unitOfWork.EventRepository.Update(updatedEvent); 
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteEvent(Guid eventId)
        {
            var deletingEvent = await _unitOfWork.EventRepository.GetSingleAsync(e => e.Id == eventId); 
            if(deletingEvent == null)
            {
                throw new NotFoundException($"Event with id {eventId} not found"); 
            }
            _unitOfWork.EventRepository.Delete(deletingEvent); 
            await _unitOfWork.SaveChangesAsync(); 
        }
    }
}