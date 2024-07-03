using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;

namespace ArtSpectrum.Services.Interface
{
    public interface IOrderDetailService
    {
        Task<OrderDetailDto> CreateOrderDetailAsync(CreateOrderDetailRequest request, CancellationToken cancellationToken);
        Task<List<OrderDetailDto>> GetAll();
        Task<OrderDetailDto> GetOrderDetailByIdAsync(int orderDetailId, CancellationToken cancellationToken);
        Task<OrderDetailDto> UpdateOrderDetailAsync(int orderDetailId, UpdateOrderDetailRequest request, CancellationToken cancellationToken);
        Task<OrderDetailDto> DeleteOrderDetailAsync(int orderDetailId, CancellationToken cancellationToken);
        Task<bool> RemovePaintingFromOrderDetailAsync(int paintingId, CancellationToken cancellationToken);

    }
}
