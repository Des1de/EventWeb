using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class GetEventByIdUseCase
    { 
        private readonly IUnitOfWork _unitOfWork;
        public GetEventByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }
        public async Task<Event?> GetEventById(Guid eventId)
        {
            return await _unitOfWork.EventRepository.GetSingleAsync(e => e.Id == eventId);
        }
    }
}