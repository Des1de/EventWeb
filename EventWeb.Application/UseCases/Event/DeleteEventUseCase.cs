using EventWeb.Application.Exceptions;
using EventWeb.Core.Abstractions.Repositories;

namespace EventWeb.Application.UseCases
{
    public class DeleteEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteEventUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
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