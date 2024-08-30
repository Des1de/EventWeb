using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class CreateParticipationUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateParticipationUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateParticipation(Participation participation)
        {
            _unitOfWork.ParticipationRepository.Create(participation); 
            await _unitOfWork.SaveChangesAsync(); 
        }
    }
}