using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;

namespace ArtSpectrum.Services.Interface
{
    public interface IPaintingService
    {
        Task<PaintingDto> CreatePaintingAsync(CreatePaintingRequest request, CancellationToken cancellationToken);
        Task<List<PaintingDto>> GetAll();
        Task<PaintingDto> GetPaintingByIdAsync(int paintingId, CancellationToken cancellationToken);
        Task<PaintingDto> UpdatePaintingAsync(int paintingId, UpdatePaintingRequest request, CancellationToken cancellationToken);
        Task<PaintingDto> DeletePaintingAsync(int paintingId, CancellationToken cancellationToken);

    }
}
