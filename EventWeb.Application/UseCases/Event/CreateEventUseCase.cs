using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class CreateEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateEventUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }
        public async Task CreateEvent(Event newEvent)
        {
            _unitOfWork.EventRepository.Create(newEvent);
            await _unitOfWork.SaveChangesAsync(); 
        }
    }
}