namespace ArtSpectrum.Contracts.Request
{
    public class CreateCartRequest
    {
        public int UserId { get; set; }
        public List<PaintingQuantity> PaintingQuantity { get; set; } = new List<PaintingQuantity>();
    }

    public class PaintingQuantity
    {
        public int PaintingId { get; set; }
        public int Quantity { get; set; }
    }
}
