namespace ArtSpectrum.Contracts.Request
{
    public class UpdateReviewRequest
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
