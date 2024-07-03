using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Exceptions;
using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Services.Interface;
using AutoMapper;
using FluentValidation;

namespace ArtSpectrum.Services.Implementation
{
    public class PaintingService : IPaintingService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        private readonly IValidator<CreatePaintingRequest> _addPaintingValidator;
        private readonly IValidator<UpdatePaintingRequest> _updatePaintingValidator;
        private readonly ICartService _cartService;

        public PaintingService(IUnitOfWork uow, IMapper mapper, IValidator<CreatePaintingRequest> addPaintingValidator, IValidator<UpdatePaintingRequest> updatePaintingValidator, ICartService cartService)
        {
            _uow = uow;
            _mapper = mapper;
            _addPaintingValidator = addPaintingValidator;
            _updatePaintingValidator = updatePaintingValidator;
            _cartService = cartService;

        }
        public async Task<PaintingDto> CreatePaintingAsync(CreatePaintingRequest request, CancellationToken cancellationToken)
        {
            var validation = await _addPaintingValidator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new RequestValidationException(validation.Errors);
            }
            var painting = await _uow.PaintingRepository.FirstOrDefaultAsync(x => x.ArtistsId == request.ArtistsId, cancellationToken);

            var paintingEntity = new Painting()
            {
                ArtistsId = request.ArtistsId,
                SaleId = request.SaleId,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                ImageUrl = request.ImageUrl,
                SalesPrice = request.SalesPrice ?? 0,
            };

            var result = await _uow.PaintingRepository.AddAsync(paintingEntity);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<PaintingDto>(result);

        }

        public async Task<PaintingDto> DeletePaintingAsync(int paintingId, CancellationToken cancellationToken)
        {
            var painting = await _uow.PaintingRepository.FirstOrDefaultAsync(x => x.PaintingId == paintingId, cancellationToken);
            if (painting is null)
            {
                throw new KeyNotFoundException("Painting not found. ");
            }
            else
            {
                await _cartService.RemovePaintingFromAllCartsAsync(painting.PaintingId, cancellationToken);
                _uow.PaintingRepository.Delete(painting);                
                await _uow.Commit(cancellationToken);
            }
            return _mapper.Map<PaintingDto>(painting);
        }

        public async Task<List<PaintingDto>> GetAll()
        {
            var result = await _uow.PaintingRepository.GetAll();
            return _mapper.Map<List<PaintingDto>>(result);  
        }

        public async Task<PaintingDto> GetPaintingByIdAsync(int paintingId, CancellationToken cancellationToken)
        {
            var result = await _uow.PaintingRepository.FirstOrDefaultAsync(x => x.PaintingId == paintingId, cancellationToken);
            if (result is null)
            {
                throw new KeyNotFoundException("Painting not found. ");
            }
            return _mapper.Map<PaintingDto>(result);
        
        }

        public async Task<PaintingDto> UpdatePaintingAsync(int paintingId, UpdatePaintingRequest request, CancellationToken cancellationToken)
        {
            var validation = await _updatePaintingValidator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new RequestValidationException(validation.Errors);
            }
            var painting = await _uow.PaintingRepository.FirstOrDefaultAsync(x => x.PaintingId == paintingId, cancellationToken);
            if (painting is null)
            {
                throw new KeyNotFoundException("Painting not found. ");
            }
            painting.SaleId = request.SaleId;
            painting.Title = request.Title;
            painting.Description = request.Description;
            painting.Price = request.Price;
            painting.StockQuantity = request.StockQuantity;
            painting.ImageUrl = request.ImageUrl;
            painting.SalesPrice = request.SalesPrice ?? 0;

            _uow.PaintingRepository.Update(painting);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<PaintingDto>(painting);
        }
    }
}
