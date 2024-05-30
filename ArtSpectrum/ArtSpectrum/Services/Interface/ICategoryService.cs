using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;

namespace ArtSpectrum.Services.Interface
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken);
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken);
        Task<CategoryDto> UpdateCategoryAsync(int categoryId, UpdateCategoryRequest request, CancellationToken cancellationToken);
        Task<CategoryDto> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken);
    }
}
