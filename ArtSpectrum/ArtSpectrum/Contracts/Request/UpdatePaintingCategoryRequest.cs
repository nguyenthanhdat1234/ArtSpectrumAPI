namespace ArtSpectrum.Contracts.Request
{
    public class UpdatePaintingCategoryRequest
    {
        public int? PaintingId { get; set; }
        public int? CategoryId { get; set; }
    }
}
