using ArtSpectrum.Mappings;
using ArtSpectrum.Repository.Models;

namespace ArtSpectrum.DTOs
{
    public class ArtistDto : IMapFrom<Artist>
    {
        public int ArtistId { get; set; }
        public int UserId { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
        public bool? Approved { get; set; } = false;
    }
}
