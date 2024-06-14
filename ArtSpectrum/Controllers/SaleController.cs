using ArtSpectrum.Attributes;
using ArtSpectrum.Commons;
using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace ArtSpectrum.Controllers
{
    public class SaleController : ApiControllerBase
    {
        private readonly ISaleService _service;
        public SaleController(ISaleService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<Result<List<SaleDto>>>> GetAllSale()
        {
            var result = await _service.GetAll();
            return Ok(Result<List<SaleDto>>.Succeed(result));
        }
        [HttpGet("{saleId:int}")]
        public async Task<ActionResult<Result<SaleDto>>> GetSaleById([FromRoute] int saleId)
        {
            var result = await _service.GetSaleByIdAsync(saleId, new CancellationToken());
            return Ok(Result<SaleDto>.Succeed(result));
        }
        [HttpPost]
        [AdminAuthorize]
        public async Task<ActionResult<Result<SaleDto>>> CreateSale([FromBody] CreateSaleRequest request)
        {
            var result = await _service.CreateSaleAsync(request, new CancellationToken());
            return Ok(Result<SaleDto>.Succeed(result));
        }
        [HttpPut("{saleId:int}")]
        [AdminAuthorize]
        public async Task<ActionResult<Result<SaleDto>>> UpdateSale(int saleId, UpdateSaleRequest request)
        {
            var result = await _service.UpdateSaleAsync(saleId, request, new CancellationToken());
            return Ok(Result<SaleDto>.Succeed(result));
        }
        [HttpDelete("{saleId:int}")]
        [AdminAuthorize]
        public async Task<ActionResult<Result<SaleDto>>> DeleteSaleById([FromRoute] int saleId)
        {
            var result = await _service.DeleteSaleAsync(saleId, new CancellationToken());
            return Ok(Result<SaleDto>.Succeed(result));
        }
  
    }
}
