namespace ArtSpectrum.Contracts.Request
{
    public class CreateCartRequest
    {
        public int UserId { get; set; }
        public int PaintingId { get; set; }
        public int Quantity { get; set; }
    }
}
