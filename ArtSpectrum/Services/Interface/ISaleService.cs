using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;

namespace ArtSpectrum.Services.Interface
{
    public interface ISaleService
    {
        Task<SaleDto> CreateSaleAsync(CreateSaleRequest request, CancellationToken cancellationToken);
        Task<List<SaleDto>> GetAll();
        Task<SaleDto> GetSaleByIdAsync(int saleId, CancellationToken cancellationToken);
        Task<SaleDto> UpdateSaleAsync(int saleId, UpdateSaleRequest request, CancellationToken cancellationToken);
        Task<SaleDto> DeleteSaleAsync(int saleId, CancellationToken cancellationToken);

    }
}
