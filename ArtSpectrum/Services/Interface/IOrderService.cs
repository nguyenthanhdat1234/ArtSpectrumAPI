using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;

namespace ArtSpectrum.Services.Interface
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken);
        Task<List<OrderDto>> GetAll();
        Task<OrderDto> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken);
        Task<OrderDto> UpdateOrderAsync(int orderId, UpdateOrderRequest request, CancellationToken cancellationToken);
        Task<OrderDto> DeleteOrderAsync(int orderId, CancellationToken cancellationToken);
    }
}
