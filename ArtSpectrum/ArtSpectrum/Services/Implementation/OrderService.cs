using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Services.Interface;
using AutoMapper;
using System.Runtime.InteropServices;

namespace ArtSpectrum.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        public OrderService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderRepository.FirstOrDefaultAsync(x => x.UserId == request.UserId);
            var orderEntity = new Order()
            {
                UserId = request.UserId,
                OrderDate = request.OrderDate,
                Status = request.Status,
            };
            var result = await _uow.OrderRepository.AddAsync(orderEntity);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<OrderDto>(result);

        }

        public async Task<OrderDto> DeleteOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderRepository.FirstOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);
            if (order is null)
            {
                throw new KeyNotFoundException("Order not found. ");
            }
            else
            {
                _uow.OrderRepository.Delete(order);
                await _uow .Commit(cancellationToken);
            }
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDto>> GetAll()
        {
            var result = await _uow.OrderRepository.GetAll();
            return _mapper.Map<List<OrderDto>>(result);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken)
        {
            var result = await _uow.OrderRepository.FirstOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);  
            if (result is null)
            {
                throw new KeyNotFoundException("Order not found. ");
            }
            return _mapper.Map<OrderDto>(result) ;
        }

        public async Task<OrderDto> UpdateOrderAsync(int orderId, UpdateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderRepository.FirstOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);
            if (order is null)
            {
                throw new KeyNotFoundException("Order not found. ");
            }
            order.Status = request.Status;

            _uow.OrderRepository.Update(order);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<OrderDto>(order);
        }
    }
}
