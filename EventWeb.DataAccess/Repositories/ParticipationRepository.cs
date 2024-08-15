using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;
using EventWeb.DataAccess.Contexts;

namespace EventWeb.DataAccess.Repositories
{
    public class ParticipationRepository : BaseRepository<Participation>, IParticipationRepository
    {
        public ParticipationRepository(EventContext context) : base(context)
        {
        }
    }
}