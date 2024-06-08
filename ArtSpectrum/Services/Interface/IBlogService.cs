using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;

namespace ArtSpectrum.Services.Interface
{
    public interface IBlogService
    {
        Task<BlogDto> CreateBlogAsync(CreateBlogRequest request, CancellationToken cancellationToken);
        Task<List<BlogDto>> GetAll();


        Task<BlogDto> GetBlogByIdAsync(int blogId, CancellationToken cancellationToken);
        Task<BlogDto> UpdateBlogAsync(int blogId, UpdateBlogRequest request, CancellationToken cancellationToken);
        Task<BlogDto> DeleteBlogByIdAsync(int blogId, CancellationToken cancellationToken);
    }
}
