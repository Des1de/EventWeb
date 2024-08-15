using EventWeb.Application.Exceptions;
using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Abstractions.Services;
using EventWeb.Core.Models;
using Microsoft.Extensions.Logging;

namespace EventWeb.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(IUnitOfWork unitOfWork, ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger; 
        } 

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _unitOfWork.CategoryRepository.GetRangeAsync(c => true); 
        }

        public async Task<Category?> GetCategoryById(Guid categoryId)
        {
            return await _unitOfWork.CategoryRepository.GetSingleAsync(c => c.Id == categoryId); 
        }

        public async Task<Category?> GetCategoryByName(string Name)
        {
            return await _unitOfWork.CategoryRepository.GetSingleAsync(c => c.Name == Name); 
        }

        public async Task CreateCategory(Category newCategory)
        {
            _unitOfWork.CategoryRepository.Create(newCategory);
            await _unitOfWork.SaveChangesAsync(); 
        }

        public async Task UpdateCategory(Guid categoryId, Category updatedCategory)
        {
            var originalCategory = await _unitOfWork.CategoryRepository.GetSingleAsync(c => c.Id == categoryId);
            if(originalCategory == null)
            {
                throw new NotFoundException($"Category with id {categoryId} not found"); 
            }
            updatedCategory.Id = categoryId; 
            _unitOfWork.CategoryRepository.Update(updatedCategory);
            await _unitOfWork.SaveChangesAsync(); 
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            var deletingCategory = await _unitOfWork.CategoryRepository.GetSingleAsync(c => c.Id == categoryId);
            if(deletingCategory == null)
            {
                throw new NotFoundException($"Category with id {categoryId} not found"); 
            }
            _unitOfWork.CategoryRepository.Delete(deletingCategory); 
            await _unitOfWork.SaveChangesAsync(); 
        }

    }
}