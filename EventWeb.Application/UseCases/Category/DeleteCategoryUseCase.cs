using EventWeb.Application.Exceptions;
using EventWeb.Core.Abstractions.Repositories;
namespace EventWeb.Application.UseCases
{
    public class DeleteCategoryUseCase
    {
        private readonly IUnitOfWork _unitOfWork; 
        public DeleteCategoryUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
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