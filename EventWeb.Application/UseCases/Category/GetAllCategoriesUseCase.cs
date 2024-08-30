using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class GetAllCategoriesUseCase
    {
        private readonly IUnitOfWork _unitOfWork; 
        public GetAllCategoriesUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _unitOfWork.CategoryRepository.GetRangeAsync(c => true); 
        }
    }
}