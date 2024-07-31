namespace ArtSpectrum.DTOs
{
    public class OrderSuccessfully
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public int UserId { get; set; }
        public string UserName { get; set; }

        public int PaintingId { get; set; }
        public string PaintingName { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtOrderTime { get; set; }
    }
}
