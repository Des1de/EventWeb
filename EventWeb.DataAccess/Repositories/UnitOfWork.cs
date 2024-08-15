using EventWeb.Core.Abstractions.Repositories;
using EventWeb.DataAccess.Contexts;

namespace EventWeb.DataAccess.Repositories
{
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventContext _eventContext; 
        private readonly Lazy<ICategoryRepository> _categoryRepository; 
        private readonly Lazy<IEventRepository> _eventRepository; 
        private readonly Lazy<IParticipationRepository> _participationRepository;
        public UnitOfWork(EventContext context)
        {
            _eventContext = context; 
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_eventContext)); 
            _eventRepository = new Lazy<IEventRepository>(() => new EventRepository(_eventContext)); 
            _participationRepository = new Lazy<IParticipationRepository>(() => new ParticipationRepository(_eventContext)); 
        }
        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository.Value; 
            }
        }
        public IEventRepository EventRepository
        {
            get
            {
                return _eventRepository.Value; 
            }
        }
        public IParticipationRepository ParticipationRepository
        {
            get
            {
                return _participationRepository.Value; 
            }
        }
        public async Task SaveChangesAsync()
        {
            await _eventContext.SaveChangesAsync(); 
        }
    }
}