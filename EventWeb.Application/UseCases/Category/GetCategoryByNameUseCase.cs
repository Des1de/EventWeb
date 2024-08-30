using EventWeb.Core.Abstractions.Repositories;
using EventWeb.Core.Models;

namespace EventWeb.Application.UseCases
{
    public class GetCategoryByNameUseCase
    {
        private readonly IUnitOfWork _unitOfWork; 
        public GetCategoryByNameUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }
        public async Task<Category?> GetCategoryByName(string Name)
        {
            return await _unitOfWork.CategoryRepository.GetSingleAsync(c => c.Name == Name); 
        }
    }
}