namespace ArtSpectrum.Contracts.Request
{
    public class CreateSaleRequest
    {
        public int? VoucherDiscount { get; set; }
        public DateTime? StartTimeSales { get; set; }
        public DateTime? EndTimeSales { get; set; }
        public string? VoucherName { get; set; }
    }
}
