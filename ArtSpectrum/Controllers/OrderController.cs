using ArtSpectrum.Attributes;
using ArtSpectrum.Commons;
using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<ActionResult<Result<OrderDto>>> UpdateOrder(int orderId, [FromBody] UpdateOrderRequest request)
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

        [HttpPut("failure/{orderId}")]
        public async Task<IActionResult> FailureOrderStatus(int orderId, CancellationToken cancellationToken)
        {
            try
            {
                var orderDto = await _service.FailuredOrderStatus(orderId, cancellationToken);
                return Ok(orderDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("complete/{orderId}")]
        public async Task<IActionResult> CompleteOrderStatus(int orderId, CancellationToken cancellationToken)
        {
            try
            {
                var orderDto = await _service.CompletedOrderStatus(orderId, cancellationToken);
                return Ok(orderDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
