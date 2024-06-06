namespace ArtSpectrum.Contracts.Request
{
    public class CreateOrderRequest
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
