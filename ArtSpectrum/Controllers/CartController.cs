using ArtSpectrum.Commons;
using ArtSpectrum.Contracts.Request;
using ArtSpectrum.Contracts.Response;
using ArtSpectrum.DTOs;
using ArtSpectrum.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ArtSpectrum.Controllers
{
    public class CartController : ApiControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<CartDto>>>> GetAllCart()
        {
            var result = await _service.GetAll();
            return Ok(Result<List<CartDto>>.Succeed(result));
        }
        [HttpGet("{userId:int}")]
        public async Task<ActionResult<Result<CartDto>>> GetArtistById([FromRoute] int userId)
        {
            var result = await _service.GetCartByIdAsync(userId, new CancellationToken());
            return Ok(Result<List<CartDto>>.Succeed(result));
        }

        [HttpPost]
        /*[ValidateRequest(typeof(CreateUserRequest))]*/
        public async Task<ActionResult<Result<ResponseCart>>> CreateArtist([FromBody] CreateCartRequest request)
        {
            var result = await _service.CreateCartAsync(request, new CancellationToken());
            return Ok(Result<List<ResponseCart>>.Succeed(result));
        }

        [HttpPut("{cartId:int}")]
        /*[ValidateRequest(typeof(UpdateUserRequest))]*/
        public async Task<ActionResult<Result<CartDto>>> UpdateArtist(int cartId, UpdateCartRequest request)
        {
            var result = await _service.UpdateCartAsync(cartId, request, new CancellationToken());
            return Ok(Result<CartDto>.Succeed(result));
        }

        [HttpDelete("{cartId:int}")]
        public async Task<ActionResult<Result<CartDto>>> DeleteArtistById([FromRoute] int cartId)
        {
            var result = await _service.DeleteCartByIdAsync(cartId, new CancellationToken());
            return Ok(Result<CartDto>.Succeed(result));
        }
    }
}
