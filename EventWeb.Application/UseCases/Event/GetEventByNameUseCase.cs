using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class GetEventByNameUseCase
    { 
        private readonly IUnitOfWork _unitOfWork;
        public GetEventByNameUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }
        public async Task<Event?> GetEventByName(string name)
        {
            return await _unitOfWork.EventRepository.GetSingleAsync(e => e.Name == name);
        }
    }
}