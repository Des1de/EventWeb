using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class GetParticipationsByUserIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetParticipationsByUserIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Participation>> GetParticipationsByUserId(Guid userId)
        {
            return await _unitOfWork.ParticipationRepository.GetRangeAsync(e => e.UserId == userId);  
        }
    }
}