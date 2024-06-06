using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;

namespace ArtSpectrum.Services.Interface
{
    public interface IReviewService
    {
        Task<ReviewDto> CreateReviewAsync(CreateReviewRequest request, CancellationToken cancellationToken);
        Task<List<ReviewDto>> GetAll();
        Task<ReviewDto> GetReviewByIdAsync(int reviewId, CancellationToken cancellationToken);
        Task<ReviewDto> UpdateReviewAsync(int reviewId, UpdateReviewRequest request, CancellationToken cancellationToken);
        Task<ReviewDto> DeleteReviewAsync(int reviewId, CancellationToken cancellationToken);
    }
}
