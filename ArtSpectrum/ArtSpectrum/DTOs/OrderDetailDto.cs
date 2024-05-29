using ArtSpectrum.Mappings;
using ArtSpectrum.Repository.Models;

namespace ArtSpectrum.DTOs
{
    public class OrderDetailDto : IMapFrom<OrderDetail>
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int PaintingId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtOrderTime { get; set; }
    }
}
