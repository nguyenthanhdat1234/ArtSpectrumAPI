using ArtSpectrum.Attributes;
using ArtSpectrum.Commons;
using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ArtSpectrum.Controllers
{
    public class OrderDetailController : ApiControllerBase
    {
        private readonly IOrderDetailService _service;

        public OrderDetailController(IOrderDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        [AdminAuthorize]
        public async Task<ActionResult<Result<List<OrderDetailDto>>>> GetAllOrderDetail()
        {
            var result = await _service.GetAll();
            return Ok(Result<List<OrderDetailDto>>.Succeed(result));
        }
        [HttpGet("{orderDetailId:int}")]
        public async Task<ActionResult<Result<OrderDetailDto>>> GetOrderDetailById([FromRoute] int orderDetailId)
        {
            var result = await _service.GetOrderDetailByIdAsync(orderDetailId, new CancellationToken());
            return Ok(Result<OrderDetailDto>.Succeed(result));
        }

        [HttpPost]
        /*[ValidateRequest(typeof(CreateUserRequest))]*/
        public async Task<ActionResult<Result<OrderDetailDto>>> CreatteOrderDetail([FromBody] CreateOrderDetailRequest request)
        {
            var result = await _service.CreateOrderDetailAsync(request, new CancellationToken());
            return Ok(Result<OrderDetailDto>.Succeed(result));
        }

        [HttpPut("{orderDetailId:int}")]
        /*[ValidateRequest(typeof(UpdateUserRequest))]*/
        public async Task<ActionResult<Result<OrderDetailDto>>> UpdateOrderDetail(int orderDetailId, UpdateOrderDetailRequest request)
        {
            var result = await _service.UpdateOrderDetailAsync(orderDetailId, request, new CancellationToken());
            return Ok(Result<OrderDetailDto>.Succeed(result));
        }

        [HttpDelete("{orderDetailId:int}")]
        [AdminAuthorize]
        public async Task<ActionResult<Result<OrderDetailDto>>> DeleteOrderDetailById([FromRoute] int orderDetailId)
        {
            var result = await _service.DeleteOrderDetailAsync(orderDetailId, new CancellationToken());
            return Ok(Result<OrderDetailDto>.Succeed(result));
        }
    }
}
