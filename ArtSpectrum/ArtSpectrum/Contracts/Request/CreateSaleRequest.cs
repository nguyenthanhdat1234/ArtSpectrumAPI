﻿namespace ArtSpectrum.Contracts.Request
{
    public class CreateSaleRequest
    {
        public decimal? VoucherDiscount { get; set; }
        public DateTime? StartTimeSales { get; set; }
        public DateTime? EndTimeSales { get; set; }
        public string? VoucherName { get; set; }
    }
}
