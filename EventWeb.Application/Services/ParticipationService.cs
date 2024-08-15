using EventWeb.Application.Exceptions;
using EventWeb.Core.Abstractions;
using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;
using Microsoft.Extensions.Logging;

namespace EventWeb.Application.Services
{
    public class ParticipationService : IParticipationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ParticipationService> _logger;
        public ParticipationService(IUnitOfWork unitOfWork, ILogger<ParticipationService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger; 
        } 

        public async Task<IEnumerable<Participation>> GetParticipationsByUserId(Guid userId)
        {
            return await _unitOfWork.ParticipationRepository.GetRangeAsync(e => e.UserId == userId);  
        }

        public async Task<IEnumerable<Participation>> GetParticipationsByEventId(Guid eventId)
        {
            return await _unitOfWork.ParticipationRepository.GetRangeAsync(e => e.EventId == eventId);
        }

        public async Task<Participation?> GetParticipationByUserAndEventId(Guid userId, Guid eventId)
        {
            return await _unitOfWork.ParticipationRepository.GetSingleAsync(e => e.UserId == userId && e.EventId == eventId);
        }

        public async Task CreateParticipation(Participation participation)
        {
            _unitOfWork.ParticipationRepository.Create(participation); 
            await _unitOfWork.SaveChangesAsync(); 
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