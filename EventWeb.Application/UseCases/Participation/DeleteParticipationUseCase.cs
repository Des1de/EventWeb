using EventWeb.Application.Exceptions;
using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class DeleteParticipationUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteParticipationUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteParticipation(Guid participationId)
        {
            var deletignParticipation = await _unitOfWork.ParticipationRepository.GetSingleAsync(p => p.Id == participationId); 
            if(deletignParticipation == null)
            {
                throw new NotFoundException($"Participation with id {participationId} not found"); 
            }

            _unitOfWork.ParticipationRepository.Delete(deletignParticipation);
            await _unitOfWork.SaveChangesAsync(); 
        }
    }
}