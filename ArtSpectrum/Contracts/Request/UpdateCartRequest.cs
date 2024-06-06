namespace ArtSpectrum.Contracts.Request
{
    public class UpdateCartRequest
    {
        public int UserId { get; set; }
        public int PaintingId { get; set; }
        public int Quantity { get; set; }
    }
}
