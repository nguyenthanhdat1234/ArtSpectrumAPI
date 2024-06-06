namespace ArtSpectrum.Contracts.Request
{
    public class UpdatePaintingRequest
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public int? SalesPrice { get; set; }
        public int SaleId { get; set; }
    }
}
