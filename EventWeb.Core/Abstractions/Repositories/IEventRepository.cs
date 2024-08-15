using System.Linq.Expressions;
using EventWeb.Core.Models;
using EventWeb.Core.Models.Parameters;

namespace EventWeb.Core.Abstractions.Repositories
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        Task<IEnumerable<Event>> GetEventsPaging(Expression<Func<Event, bool>> condition, EventParameters parameters);
    }
}