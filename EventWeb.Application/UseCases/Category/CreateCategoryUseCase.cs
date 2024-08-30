using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class CreateCategoryUseCase
    {
        private readonly IUnitOfWork _unitOfWork; 
        public CreateCategoryUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }
        public async Task CreateCategory(Category newCategory)
        {
            _unitOfWork.CategoryRepository.Create(newCategory);
            await _unitOfWork.SaveChangesAsync(); 
        }

    }
}