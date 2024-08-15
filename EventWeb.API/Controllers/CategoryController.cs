using AutoMapper;
using EventWeb.API.DTOs;
using EventWeb.Core.Abstractions.Services;
using EventWeb.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService; 
        private readonly IMapper _mapper; 

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper; 
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            var response = _mapper.Map<IEnumerable<CategoryResponseDTO>>(categories);
            return Ok(response); 
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryById(id);
            var response = _mapper.Map<CategoryResponseDTO>(category);
            return Ok(response); 
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequestDTO request)
        {
            var category = _mapper.Map<Category>(request); 
            await _categoryService.CreateCategory(category); 
            return NoContent(); 
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryRequestDTO request)
        {
            var category = _mapper.Map<Category>(request); 
            await _categoryService.UpdateCategory(id, category); 
            return NoContent(); 
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoryService.DeleteCategory(id); 
            return NoContent();
        }
    }
}