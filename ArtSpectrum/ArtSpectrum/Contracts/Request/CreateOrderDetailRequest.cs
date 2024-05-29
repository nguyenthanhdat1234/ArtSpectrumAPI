namespace ArtSpectrum.Contracts.Request
{
    public class CreateOrderDetailRequest
    {
        public int OrderId { get; set; }
        public int PaintingId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtOrderTime { get; set; }
    }
}
