using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;
using EventWeb.DataAccess.Contexts;

namespace EventWeb.DataAccess.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(EventContext context) : base(context)
        {
        }
    }
}