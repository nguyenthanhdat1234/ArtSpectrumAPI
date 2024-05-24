using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;

namespace ArtSpectrum.Services.Interface
{
    public interface IArtistService
    {
        Task<ArtistDto> CreateArtistAsync(CreateArtistRequest request, CancellationToken cancellationToken);
        Task<List<ArtistDto>> GetAll();


        Task<ArtistDto> GetArtistByIdAsync(int artistId, CancellationToken cancellationToken);
        Task<ArtistDto> UpdateArtistAsync(int artistId, UpdateArtistRequest request, CancellationToken cancellationToken);
        Task<ArtistDto> DeleteArtistByIdAsync(int artistId, CancellationToken cancellationToken);
    }
}
