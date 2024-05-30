namespace ArtSpectrum.Contracts.Request
{
    public class CreateReviewRequest
    {
        public int UserId { get; set; }
        public int PaintingId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
