using ArtSpectrum.Attributes;
using ArtSpectrum.Commons;
using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ArtSpectrum.Controllers
{
    public class CategoryController : ApiControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<Result<List<CategoryDto>>>> GetAllCategory()
        {
            var result = await _service.GetAll();
            return Ok(Result<List<CategoryDto>>.Succeed(result));
        }
        [HttpGet("{categoryId:int}")]
        public async Task<ActionResult<Result<CategoryDto>>> GetCategoryById([FromRoute] int categoryId)
        {
            var result = await _service.GetCategoryByIdAsync(categoryId, new CancellationToken());
            return Ok(Result<CategoryDto>.Succeed(result));
        }
        [HttpPost]
        [AdminAuthorize]
        public async Task<ActionResult<Result<CategoryDto>>> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var result = await _service.CreateCategoryAsync(request, new CancellationToken());
            return Ok(Result<CategoryDto>.Succeed(result));
        }
        [HttpPut("{categoryId:int}")]
        [AdminAuthorize]
        public async Task<ActionResult<Result<CategoryDto>>> UpdateCategory(int categoryId, UpdateCategoryRequest request)
        {
            var result = await _service.UpdateCategoryAsync(categoryId, request, new CancellationToken());
            return Ok(Result<CategoryDto>.Succeed(result));
        }
        [HttpDelete("{categoryId:int}")]
        [AdminAuthorize]
        public async Task<ActionResult<Result<CategoryDto>>> DeleteCategoryById([FromRoute] int categoryId)
        {
            var result = await _service.DeleteCategoryAsync(categoryId, new CancellationToken());
            return Ok(Result<CategoryDto>.Succeed(result));
        }
    }
}
