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


        public async Task<OrderDto> CompletedOrderStatus(int orderId, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderRepository.FirstOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found!");
            }

            if (order.Status.ToLower() == "completed")
            {
                throw new InvalidOperationException("Order is already completed.");
            }

            order.Status = "Completed";

            _uow.OrderRepository.Update(order);
            await _uow.Commit(cancellationToken);

            var orderDetails = await _uow.OrderDetailRepository.WhereAsync(x => x.OrderId == orderId, cancellationToken);

            foreach (var item in orderDetails)
            {
                var quantity = item.Quantity;
                var product = await _uow.PaintingRepository.FirstOrDefaultAsync(x => x.PaintingId == item.PaintingId, cancellationToken);

                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {item.PaintingId} not found.");
                }

                if (product.StockQuantity <= quantity)
                {
                    product.StockQuantity = 0;
                }
                else
                {
                    product.StockQuantity -= quantity;
                }

                _uow.PaintingRepository.Update(product);
            }

            await _uow.Commit(cancellationToken);

            return _mapper.Map<OrderDto>(order);
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

        public async Task<OrderDto> FailuredOrderStatus(int orderId, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderRepository.FirstOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found!");
            }

            order.Status = "Canceled";

            var result = _uow.OrderRepository.Update(order);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<OrderDto>(result);
        }

        public async Task<List<OrderDto>> GetAll()
        {
            var result = await _uow.OrderRepository.GetAll();
            return _mapper.Map<List<OrderDto>>(result);
        }

        public async Task<List<OrderSuccessfully>> GetAllOrderCompleted()
        {
            var listOrderSuccessfull = new List<OrderSuccessfully>();

            try
            {
                var orderItemCompleted = await _uow.OrderRepository.WhereAsync(x => x.Status.ToLower().Equals("completed"));

                foreach (var item in orderItemCompleted)
                {
                    var orderDetailCompleted = await _uow.OrderDetailRepository.WhereAsync(x => x.OrderId == item.OrderId);

                    foreach (var itemOrderDetail in orderDetailCompleted)
                    {
                        var paintingId = itemOrderDetail.PaintingId;
                        var painting = await _uow.PaintingRepository.FirstOrDefaultAsync(x => x.PaintingId == paintingId);
                        var paintingName = painting?.Title ?? "Unknown";
                        var user = await _uow.UserRepository.FirstOrDefaultAsync(x => x.UserId == item.UserId);
                        var userName = user?.FullName ?? "Unknown";

                        var orderDetailSucess = new OrderSuccessfully
                        {
                            OrderId = item.OrderId,
                            OrderDate = item.OrderDate,
                            UserId = item.UserId,
                            UserName = userName,
                            PaintingId = paintingId,
                            PaintingName = paintingName,
                            PriceAtOrderTime = itemOrderDetail.PriceAtOrderTime,
                            Quantity = itemOrderDetail.Quantity,
                            Status = item.Status,
                        };

                        listOrderSuccessfull.Add(orderDetailSucess);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving completed orders.", ex);
            }

            return listOrderSuccessfull;
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

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            if (order.Status.ToLower() == "completed")
            {
                throw new InvalidOperationException("Cannot update a completed order.");
            }

            order.Status = request.Status;

            _uow.OrderRepository.Update(order);
            await _uow.Commit(cancellationToken);

            return _mapper.Map<OrderDto>(order);
        }

    }
}
