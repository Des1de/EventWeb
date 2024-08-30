using EventWeb.Application.Exceptions;
using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class UpdateCategoryUseCase
    {
        private readonly IUnitOfWork _unitOfWork; 
        public UpdateCategoryUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
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

    }
}