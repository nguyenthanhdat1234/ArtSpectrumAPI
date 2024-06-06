using ArtSpectrum.Commons;
using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ArtSpectrum.Controllers
{
    public class ReviewController : ApiControllerBase
    {
        private readonly IReviewService _service;
        public ReviewController(IReviewService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<Result<List<ReviewDto>>>> GetAllReview()
        {
            var result = await _service.GetAll();
            return Ok(Result<List<ReviewDto>>.Succeed(result));
        }
        [HttpGet("{reviewId:int}")]
        public async Task<ActionResult<Result<ReviewDto>>> GetReviewById([FromRoute] int reviewId)
        {
            var result = await _service.GetReviewByIdAsync(reviewId, new CancellationToken());
            return Ok(Result<ReviewDto>.Succeed(result));
        }
        [HttpPost]
        public async Task<ActionResult<Result<ReviewDto>>> CreateReview([FromBody] CreateReviewRequest request)
        {
            var result = await _service.CreateReviewAsync(request, new CancellationToken());
            return Ok(Result<ReviewDto>.Succeed(result));
        }
        [HttpPut("{reviewId:int}")]
        public async Task<ActionResult<Result<ReviewDto>>> UpdateReview(int reviewId, UpdateReviewRequest request)
        {
            var result = await _service.UpdateReviewAsync(reviewId, request, new CancellationToken());
            return Ok(Result<ReviewDto>.Succeed(result));
        }
        [HttpDelete("{reviewId:int}")]
        public async Task<ActionResult<Result<ReviewDto>>> DeleteReviewById([FromRoute] int reviewId)
        {
            var result = await _service.DeleteReviewAsync(reviewId, new CancellationToken());
            return Ok(Result<ReviewDto>.Succeed(result));
        }

    }
}
