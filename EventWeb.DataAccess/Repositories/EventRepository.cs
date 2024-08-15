using System.Linq.Expressions;
using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;
using EventWeb.Core.Models.Parameters;
using EventWeb.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EventWeb.DataAccess.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(EventContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Event>> GetEventsPaging(Expression<Func<Event, bool>> condition, EventParameters parameters)
        {
            return await _context.Set<Event>()
                            .Skip((parameters.PageNumber-1)*parameters.PageSize)
                            .Take(parameters.PageSize)
                            .ToListAsync(); 
        }
    }
}