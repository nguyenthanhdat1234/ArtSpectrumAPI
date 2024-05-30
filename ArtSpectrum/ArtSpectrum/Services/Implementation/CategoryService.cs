using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Services.Interface;
using AutoMapper;

namespace ArtSpectrum.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        public CategoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var categoryEntity = new Category()
            {
                CategoryName = request.CategoryName,
            };
            var result = await _uow.CategoryRepository.AddAsync(categoryEntity);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<CategoryDto>(result);
        }

        public async Task<CategoryDto> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            var category = await _uow.CategoryRepository.FirstOrDefaultAsync(x => x.CategoryId == categoryId, cancellationToken);
            if (category is null)
            {
                throw new KeyNotFoundException("Category not found. ");
            }
            else
            {
                _uow.CategoryRepository.Delete(category);
                await _uow.Commit(cancellationToken);
            }
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var result = await _uow.CategoryRepository.GetAll();
            return _mapper.Map<List<CategoryDto>>(result);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken)
        {
            var result = await _uow.CategoryRepository.FirstOrDefaultAsync(x => x.CategoryId == categoryId, cancellationToken);
            if (result is null)
            {
                throw new KeyNotFoundException("Category not found. ");
            }
            return _mapper.Map<CategoryDto>(result);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int categoryId, UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _uow.CategoryRepository.FirstOrDefaultAsync(x => x.CategoryId == categoryId, cancellationToken);
            if (category is null)
            {
                throw new KeyNotFoundException("Category not found. ");
            }
            category.CategoryName = request.CategoryName;

            _uow.CategoryRepository.Update(category);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
