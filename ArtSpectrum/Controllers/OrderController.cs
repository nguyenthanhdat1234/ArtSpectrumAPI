using ArtSpectrum.Attributes;
using ArtSpectrum.Commons;
using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ArtSpectrum.Controllers
{
    public class OrderController : ApiControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }
        [HttpGet]
        [AdminAuthorize]
        public async Task<ActionResult<Result<List<OrderDto>>>> GetAllOrder()
        {
            var result = await _service.GetAll();
            return Ok(Result<List<OrderDto>>.Succeed(result));
        }
        [HttpGet("{orderId:int}")]
        public async Task<ActionResult<Result<OrderDto>>> GetOrderById([FromRoute] int orderId)
        {
            var result = await _service.GetOrderByIdAsync(orderId, new CancellationToken());
            return Ok(Result<OrderDto>.Succeed(result));
        }
        [HttpPost]
        public async Task<ActionResult<Result<OrderDto>>> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var result = await _service.CreateOrderAsync(request, new CancellationToken());
            return Ok(Result<OrderDto>.Succeed(result));
        }
        [HttpPut("{orderId:int}")]
        public async Task<ActionResult<Result<OrderDto>>> UpdateOrder(int orderId, UpdateOrderRequest request)
        {
            var result = await _service.UpdateOrderAsync(orderId, request, new CancellationToken());
            return Ok(Result<OrderDto>.Succeed(result));
        }
        [HttpDelete("{orderId:int}")]
        [AdminAuthorize]
        public async Task<ActionResult<Result<OrderDto>>> DeleteOrderById([FromRoute] int orderId)
        {
            var result = await _service.DeleteOrderAsync(orderId, new CancellationToken());
            return Ok(Result<OrderDto>.Succeed(result));
        }
    }
}
