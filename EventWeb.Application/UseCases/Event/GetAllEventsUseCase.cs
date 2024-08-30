using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;
using EventWeb.Core.Models.Parameters;

namespace EventWeb.Application.UseCases
{
    public class GetAllEventsUseCase
    { 
        private readonly IUnitOfWork _unitOfWork;
        public GetAllEventsUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }
        public async Task<IEnumerable<Event>> GetAllEvents(EventParameters parameters)
        {
            return await _unitOfWork.EventRepository.GetEventsPaging( e => true, 
            parameters);
        }
    }
}