using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Services.Interface;
using AutoMapper;

namespace ArtSpectrum.Services.Implementation
{
    public class OrderDetailService : IOrderDetailService
    {

        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        public OrderDetailService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<OrderDetailDto> CreateOrderDetailAsync(CreateOrderDetailRequest request, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderDetailRepository.FirstOrDefaultAsync(x => x.OrderId == request.OrderId 
            && x.PaintingId == request.PaintingId && x.Quantity == request.Quantity, cancellationToken);
            var orderDetailEntity = new OrderDetail()
            {
                OrderId = request.OrderId,
                PaintingId = request.PaintingId,
                Quantity = request.Quantity,
                PriceAtOrderTime = request.PriceAtOrderTime,
            };
            var result = await _uow.OrderDetailRepository.AddAsync(orderDetailEntity);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<OrderDetailDto>(result);
        }

        public async Task<OrderDetailDto> DeleteOrderDetailAsync(int orderDetailId, CancellationToken cancellationToken)
        {
            var orderDetail = await _uow.OrderDetailRepository.FirstOrDefaultAsync(x => x.OrderDetailId == orderDetailId, cancellationToken);
            if (orderDetail is null)
            {
                throw new KeyNotFoundException("Order Detail not found. ");
            }
            else
            {
                _uow.OrderDetailRepository.Delete(orderDetail);
                await _uow.Commit(cancellationToken);
            }
            return _mapper.Map<OrderDetailDto>(orderDetail);
        }

        public async Task<List<OrderDetailDto>> GetAll()
        {
            var result = await _uow.OrderDetailRepository.GetAll();
            return _mapper.Map<List<OrderDetailDto>>(result);
        }

        public async Task<OrderDetailDto> GetOrderDetailByIdAsync(int orderDetailId, CancellationToken cancellationToken)
        {
            var result = await _uow.OrderDetailRepository.FirstOrDefaultAsync(x => x.OrderDetailId == orderDetailId, cancellationToken);
            if (result is null)
            {
                throw new KeyNotFoundException("Order Detail not found. ");
            }
            return _mapper.Map<OrderDetailDto>(result);
        }

        public async Task<OrderDetailDto> UpdateOrderDetailAsync(int orderDetailId, UpdateOrderDetailRequest request, CancellationToken cancellationToken)
        {
            var orderDetail = await _uow.OrderDetailRepository.FirstOrDefaultAsync(x => x.OrderDetailId == orderDetailId, cancellationToken);
            if (orderDetail is null)
            {
                throw new KeyNotFoundException("Order not found. ");
            }
            orderDetail.OrderId = request.OrderId;
            orderDetail.Quantity = request.Quantity;
            orderDetail.PaintingId = request.PaintingId;
            orderDetail.PriceAtOrderTime = request.PriceAtOrderTime;

            _uow.OrderDetailRepository.Update(orderDetail);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<OrderDetailDto>(orderDetail);
        }
    }
}
