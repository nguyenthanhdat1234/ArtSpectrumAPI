using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Services.Interface;
using AutoMapper;
using System.Runtime.InteropServices;

namespace ArtSpectrum.Services.Implementation
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        public SaleService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<SaleDto> CreateSaleAsync(CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var saleEntity = new Sale()
            {
                VoucherDiscount = request.VoucherDiscount ?? 0,
                StartTimeSales = request.StartTimeSales,
                EndTimeSales = request.EndTimeSales,
                VoucherName = request.VoucherName,
            };
            var result = await _uow.SaleRepository.AddAsync(saleEntity);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<SaleDto>(result);
        }
        public async Task<SaleDto> DeleteSaleAsync(int saleId, CancellationToken cancellationToken)
        {
            var sale = await _uow.SaleRepository.FirstOrDefaultAsync(x => x.SaleId == saleId, cancellationToken);
            if (sale is null)
            {
                throw new KeyNotFoundException("Sale not found");
            }
            else
            {
                _uow.SaleRepository.Delete(sale);
                await _uow.Commit(cancellationToken);
            }
            return _mapper.Map<SaleDto>(sale);
        }
       

        public async Task<List<SaleDto>> GetAll()
        {
            var result = await _uow.SaleRepository.GetAll();
            return _mapper.Map<List<SaleDto>>(result);
        }

        public async Task<SaleDto> GetSaleByIdAsync(int saleId, CancellationToken cancellationToken)
        {
            var result = await _uow.SaleRepository.FirstOrDefaultAsync(x => x.SaleId ==saleId, cancellationToken);
            if (result is null)
            {
                throw new KeyNotFoundException("Sale not found. ");

            }
            return _mapper.Map<SaleDto>(result);
        }

        public async Task<SaleDto> UpdateSaleAsync(int saleId, UpdateSaleRequest request, CancellationToken cancellationToken)
        {
            var sale = await _uow.SaleRepository.FirstOrDefaultAsync(x => x.SaleId==saleId, cancellationToken);
            if (sale is null)
            {
                throw new KeyNotFoundException("Sale not found. ");
            }
            sale.VoucherDiscount = request.VoucherDiscount ?? 0;
            sale.StartTimeSales = request.StartTimeSales;
            sale.EndTimeSales = request.EndTimeSales;
            sale.VoucherName = request.VoucherName;

            _uow.SaleRepository.Update(sale);
            await _uow.Commit(cancellationToken);
            return _mapper.Map<SaleDto>(sale);
        
        }
        
    }
}
