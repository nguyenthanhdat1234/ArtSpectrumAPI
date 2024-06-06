using ArtSpectrum.Mappings;
using ArtSpectrum.Repository.Models;

namespace ArtSpectrum.DTOs
{
    public class CartDto : IMapFrom<Cart>
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int PaintingId { get; set; }
        public int Quantity { get; set; }
    }
}
