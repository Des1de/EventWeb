namespace EventWeb.Core.Abstractions.Repositories
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IEventRepository EventRepository { get; }
        public IParticipationRepository ParticipationRepository { get; }
        public Task SaveChangesAsync(); 
    }
}