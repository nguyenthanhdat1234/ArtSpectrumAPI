using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using AutoMapper.Configuration.Conventions;

namespace ArtSpectrum.Services.Interface
{
    public interface IPaintingCategoryService
    {
        Task<PaintingCategoryDto> CreatePaintingCategoryAsync(CreatePaintingCategoryRequest request, CancellationToken cancellationToken);
        Task<List<PaintingCategoryDto>> GetAll();
        Task<PaintingCategoryDto> GetPaintingCategoryByIdAsync(int paintingCategoryId,  CancellationToken cancellationToken);
        Task<PaintingCategoryDto> UpdatePaintingCategoryAsync(int paintingCategoryId, UpdatePaintingCategoryRequest request, CancellationToken cancellationToken);
        Task<PaintingCategoryDto> DeletePaintingCategoryAsync(int paintingCategoryId, CancellationToken cancellationToken);

    }
}
