using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class GetParticipationsByUserAndEventIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetParticipationsByUserAndEventIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Participation?> GetParticipationByUserAndEventId(Guid userId, Guid eventId)
        {
            return await _unitOfWork.ParticipationRepository.GetSingleAsync(e => e.UserId == userId && e.EventId == eventId);
        }
    }
}