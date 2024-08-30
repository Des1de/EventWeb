using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class GetParticipationsByEventIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetParticipationsByEventIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Participation>> GetParticipationsByEventId(Guid eventId)
        {
            return await _unitOfWork.ParticipationRepository.GetRangeAsync(e => e.EventId == eventId);
        }
    }
}