using EventWeb.Application.Exceptions;
using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class UpdateEventUseCase
    { 
        private readonly IUnitOfWork _unitOfWork;
        public UpdateEventUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
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
    }
}