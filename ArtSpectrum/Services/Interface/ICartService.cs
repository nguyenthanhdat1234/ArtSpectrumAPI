using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Repository.Models;

namespace ArtSpectrum.Services.Interface
{
    public interface ICartService
    {
        Task<CartDto> CreateCartAsync(CreateCartRequest request, CancellationToken cancellationToken);
        Task<List<CartDto>> GetAll();


        Task<CartDto> GetCartByIdAsync(int cartId, CancellationToken cancellationToken);
        Task<CartDto> UpdateCartAsync(int cartId, UpdateCartRequest request, CancellationToken cancellationToken);
        Task<CartDto> DeleteCartByIdAsync(int cartId, CancellationToken cancellationToken);
    }
}
