using ArtSpectrum.Mappings;
using ArtSpectrum.Repository.Models;

namespace ArtSpectrum.DTOs
{
    public class CategoryDto : IMapFrom<Category>
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
