using ArtSpectrum.Commons;
using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ArtSpectrum.Controllers
{
    public class BlogController : ApiControllerBase
    {
        private readonly IBlogService _service;

        public BlogController(IBlogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<BlogDto>>>> GetAllBlog()
        {
            var result = await _service.GetAll();
            return Ok(Result<List<BlogDto>>.Succeed(result));
        }
        [HttpGet("{blogId:int}")]
        public async Task<ActionResult<Result<ArtistDto>>> GetBlogById([FromRoute] int blogId)
        {
            var result = await _service.GetBlogByIdAsync(blogId, new CancellationToken());
            return Ok(Result<BlogDto>.Succeed(result));
        }

        /*[ValidateRequest(typeof(CreateUserRequest))]*/
        [HttpPost]
        public async Task<ActionResult<Result<BlogDto>>> CreateBlog([FromBody] CreateBlogRequest request)
        {
            var result = await _service.CreateBlogAsync(request, new CancellationToken());
            return Ok(Result<BlogDto>.Succeed(result));
        }

        [HttpPut("{blogId:int}")]
        /*[ValidateRequest(typeof(UpdateUserRequest))]*/
        public async Task<ActionResult<Result<BlogDto>>> UpdateBlog(int blogId, UpdateBlogRequest request)
        {
            var result = await _service.UpdateBlogAsync(blogId, request, new CancellationToken());
            return Ok(Result<BlogDto>.Succeed(result));
        }

        [HttpDelete("{blogId:int}")]
        public async Task<ActionResult<Result<BlogDto>>> DeleteBlogById([FromRoute] int blogId)
        {
            var result = await _service.DeleteBlogByIdAsync(blogId, new CancellationToken());
            return Ok(Result<BlogDto>.Succeed(result));
        }
    }
}
