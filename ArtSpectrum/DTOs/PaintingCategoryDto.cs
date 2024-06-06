using ArtSpectrum.Mappings;
using ArtSpectrum.Repository.Models;

namespace ArtSpectrum.DTOs
{
    public class PaintingCategoryDto : IMapFrom<PaintingCategory>
    {
        public int PaintingCategoryId { get; set; }
        public int? PaintingId { get; set; }
        public int? CategoryId { get; set; }
    }
}
