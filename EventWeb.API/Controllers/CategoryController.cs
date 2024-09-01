using AutoMapper;
using EventWeb.API.DTOs;
using EventWeb.Application.UseCases;
using EventWeb.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly GetAllCategoriesUseCase _getAllCategoriesUseCase; 
        private readonly GetCategoryByIdUseCase _getCategoryByIdUseCase;
        private readonly CreateCategoryUseCase _createCategoryUseCase; 
        private readonly UpdateCategoryUseCase _updateCategoryUseCase; 
        private readonly DeleteCategoryUseCase _deleteCategoryUseCase;
        private readonly IMapper _mapper; 

        public CategoryController(IMapper mapper,
            GetAllCategoriesUseCase getAllCategoriesUseCase,
            GetCategoryByIdUseCase getCategoryByIdUseCase,
            CreateCategoryUseCase createCategoryUseCase,
            UpdateCategoryUseCase updateCategoryUseCase,
            DeleteCategoryUseCase deleteCategoryUseCase)
        {
            _getAllCategoriesUseCase = getAllCategoriesUseCase;
            _getCategoryByIdUseCase = getCategoryByIdUseCase; 
            _createCategoryUseCase = createCategoryUseCase; 
            _updateCategoryUseCase = updateCategoryUseCase; 
            _deleteCategoryUseCase = deleteCategoryUseCase; 
            _mapper = mapper; 
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _getAllCategoriesUseCase.GetAllCategories();
            var response = _mapper.Map<IEnumerable<CategoryResponseDTO>>(categories);
            return Ok(response); 
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _getCategoryByIdUseCase.GetCategoryById(id);
            var response = _mapper.Map<CategoryResponseDTO>(category);
            return Ok(response); 
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequestDTO request)
        {
            var category = _mapper.Map<Category>(request); 
            await _createCategoryUseCase.CreateCategory(category); 
            return NoContent(); 
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryRequestDTO request)
        {
            var category = _mapper.Map<Category>(request); 
            await _updateCategoryUseCase.UpdateCategory(id, category); 
            return NoContent(); 
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _deleteCategoryUseCase.DeleteCategory(id); 
            return NoContent();
        }
    }
}