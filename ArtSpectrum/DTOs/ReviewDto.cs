using ArtSpectrum.Mappings;
using ArtSpectrum.Repository.Models;

namespace ArtSpectrum.DTOs
{
    public class ReviewDto : IMapFrom<Review>
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int PaintingId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
