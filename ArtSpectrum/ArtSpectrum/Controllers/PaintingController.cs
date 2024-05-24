using ArtSpectrum.Commons;
using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ArtSpectrum.Controllers
{
    public class PaintingController : ApiControllerBase
    {
        private readonly IPaintingService _service;
        public PaintingController(IPaintingService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<Result<List<PaintingDto>>>> GetAllPainting()
        {
            var result = await _service.GetAll();
            return Ok(Result<List<PaintingDto>>.Succeed(result));
        }
        [HttpGet("{paintingId:int}")]
        public async Task<ActionResult<Result<PaintingDto>>> GetPaintingById([FromRoute] int paintingId)
        {
            var result = await _service.GetPaintingByIdAsync(paintingId, new CancellationToken());
            return Ok(Result<PaintingDto>.Succeed(result));
        }
        [HttpPost]
        public async Task<ActionResult<Result<PaintingDto>>> CreatePainting([FromBody] CreatePaintingRequest request)
        {
            var result = await _service.CreatePaintingAsync(request, new CancellationToken());
            return Ok(Result<PaintingDto>.Succeed(result));
        }
        [HttpPut("{paintingId:int}")]
        public async Task<ActionResult<Result<PaintingDto>>> UpdatePainting(int paintingId, UpdatePaintingRequest request)
        {
            var result = await _service.UpdatePaintingAsync(paintingId, request, new CancellationToken());
            return Ok(Result<PaintingDto>.Succeed(result));
        }
        [HttpDelete("{paintingId:int}")]
        public async Task<ActionResult<Result<PaintingDto>>> DeletePaintingById([FromRoute] int paintingId)
        {
            var result = await _service.DeletePaintingAsync(paintingId, new CancellationToken());
            return Ok(Result<PaintingDto>.Succeed(result));
        }
    }
}
