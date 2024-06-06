using ArtSpectrum.Commons;
using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ArtSpectrum.Controllers
{
    public class PaintingCategoryController : ApiControllerBase
    {
        private readonly IPaintingCategoryService _service;
        public PaintingCategoryController(IPaintingCategoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<Result<List<PaintingCategoryDto>>>> GetAllPaintingCategory()
        {
            var result = await _service.GetAll();
            return Ok(Result<List<PaintingCategoryDto>>.Succeed(result));
        }
        [HttpGet("{paintingCategoryId:int}")]
        public async Task<ActionResult<Result<PaintingCategoryDto>>> GetPaintingCategoryById([FromRoute] int paintingCategoryId)
        {
            var result = await _service.GetPaintingCategoryByIdAsync(paintingCategoryId, new CancellationToken());
            return Ok(Result<PaintingCategoryDto>.Succeed(result));
        }
        [HttpPost]
        public async Task<ActionResult<Result<PaintingCategoryDto>>> CreatePaintingCategory([FromBody] CreatePaintingCategoryRequest request)
        {
            var result = await _service.CreatePaintingCategoryAsync(request, new CancellationToken());
            return Ok(Result<PaintingCategoryDto>.Succeed(result));
        }
        [HttpPut("{paintingCategoryId:int}")]
        public async Task<ActionResult<Result<PaintingCategoryDto>>> UpdatePaintingCategory(int paintingCategoryId, UpdatePaintingCategoryRequest request)
        {
            var result = await _service.UpdatePaintingCategoryAsync(paintingCategoryId, request, new CancellationToken());
            return Ok(Result<PaintingCategoryDto>.Succeed(result));
        }
        [HttpDelete("{paintingCategoryId:int}")]
        public async Task<ActionResult<Result<PaintingCategoryDto>>> DeletePaintingCategoryById([FromRoute] int paintingCategoryId)
        {
            var result = await _service.DeletePaintingCategoryAsync(paintingCategoryId, new CancellationToken());
            return Ok(Result<PaintingCategoryDto>.Succeed(result));
        }
    }
}
