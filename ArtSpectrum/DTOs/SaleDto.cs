using ArtSpectrum.Mappings;
using ArtSpectrum.Repository.Models;

namespace ArtSpectrum.DTOs
{
    public class SaleDto : IMapFrom<Sale>
    {
        public int SaleId { get; set; }
        public decimal? VoucherDiscount { get; set; }
        public DateTime? StartTimeSales { get; set; }
        public DateTime? EndTimeSales { get; set; }
        public string? VoucherName { get; set; }

    }
}
