using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Services.Interface;
using AutoMapper;

namespace ArtSpectrum.Services.Implementation
{
    public class PaintingCategoryService : IPaintingCategoryService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        public PaintingCategoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PaintingCategoryDto> CreatePaintingCategoryAsync(CreatePaintingCategoryRequest request, CancellationToken cancellationToken)
        {
            var paintingcategory = await _uow.PaintingCategoryRepository.FirstOrDefaultAsync(x => x.PaintingId == request.PaintingId 
            && x.CategoryId == request.CategoryId, cancellationToken);
            var paintingCategoryEntity = new PaintingCategory()
            {
                PaintingId = request.PaintingId,
                CategoryId = request.CategoryId,

            };
            var result = await _uow.PaintingCategoryRepository.AddAsync(paintingCategoryEntity);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<PaintingCategoryDto>(result);
        }

        public async Task<PaintingCategoryDto> DeletePaintingCategoryAsync(int paintingCategoryId, CancellationToken cancellationToken)
        {
            var paintingCategory = await _uow.PaintingCategoryRepository.FirstOrDefaultAsync(x => x.PaintingCategoryId == paintingCategoryId, cancellationToken);
            if (paintingCategory is null)
            {
                throw new KeyNotFoundException("Painting Category not found. ");
            }
            else
            {
                _uow.PaintingCategoryRepository.Delete(paintingCategory);
                await _uow.Commit(cancellationToken);
            }
            return _mapper.Map<PaintingCategoryDto>(paintingCategory);

        }

        public async Task<List<PaintingCategoryDto>> GetAll()
        {
            var result = await _uow.PaintingCategoryRepository.GetAll();
            return _mapper.Map<List<PaintingCategoryDto>>(result);
        }

        public async Task<PaintingCategoryDto> GetPaintingCategoryByIdAsync(int paintingCategoryId, CancellationToken cancellationToken)
        {
            var result = await _uow.PaintingCategoryRepository.FirstOrDefaultAsync(x => x.PaintingCategoryId == paintingCategoryId, cancellationToken);
            if(result is null)
            {
                throw new KeyNotFoundException("Painting Category not found. ");
            }
            return _mapper.Map<PaintingCategoryDto>(result);
        }

        public async Task<PaintingCategoryDto> UpdatePaintingCategoryAsync(int paintingCategoryId, UpdatePaintingCategoryRequest request, CancellationToken cancellationToken)
        {
            var paintingCategory = await _uow.PaintingCategoryRepository.FirstOrDefaultAsync(x => x.PaintingCategoryId == paintingCategoryId, cancellationToken);
            if (paintingCategory is null)
            {
                throw new KeyNotFoundException("Painting Category not found. ");
            }
            paintingCategory.PaintingId = request.PaintingId;
            paintingCategory.CategoryId = request.CategoryId;

            _uow.PaintingCategoryRepository.Update(paintingCategory);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<PaintingCategoryDto>(paintingCategory);
        }
    }
}
