using EventWeb.Core.Models;
using EventWeb.Core.Models.Parameters;

namespace EventWeb.Core.Abstractions
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEvents(EventParameters parameters); 
        Task<Event?> GetEventById(Guid eventId);
        Task<Event?> GetEventByName(string name);
        Task CreateEvent(Event newEvent);
        Task UpdateEvent(Guid eventId, Event updatedEvent);
        Task DeleteEvent(Guid eventId);
    }
}