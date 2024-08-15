using EventWeb.Core.Models;

namespace EventWeb.Core.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();

        Task<Category?> GetCategoryById(Guid categoryId);

        Task<Category?> GetCategoryByName(string Name);

        Task CreateCategory(Category newCategory);

        Task UpdateCategory(Guid categoryId, Category updatedCategory);

        Task DeleteCategory(Guid categoryId);

    }
}