using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class GetCategoryByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork; 
        public GetCategoryByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }
        public async Task<Category?> GetCategoryById(Guid categoryId)
        {
            return await _unitOfWork.CategoryRepository.GetSingleAsync(c => c.Id == categoryId); 
        }
    }
}