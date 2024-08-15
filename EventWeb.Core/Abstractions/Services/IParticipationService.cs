using EventWeb.Core.Models;

namespace EventWeb.Core.Abstractions
{
    public interface IParticipationService
    {
        Task<IEnumerable<Participation>> GetParticipationsByUserId(Guid userId);
        Task<IEnumerable<Participation>> GetParticipationsByEventId(Guid eventId);
        Task<Participation?> GetParticipationByUserAndEventId(Guid userId, Guid eventId);
        Task CreateParticipation(Participation participation);
        Task DeleteParticipation(Guid participationId);
    }
}