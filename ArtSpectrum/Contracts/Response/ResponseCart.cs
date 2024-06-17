using ArtSpectrum.Contracts.Request;

namespace ArtSpectrum.Contracts.Response
{
    public class ResponseCart
    {
            public int UserId { get; set; }
            public List<PaintingQuantity> PaintingQuantity { get; set; } = new List<PaintingQuantity>();
            public string Title { get; set; } = null!;
            public string? Description { get; set; }
            public int Price { get; set; }
            public string? ImageUrl { get; set; }
            public int? SalesPrice { get; set; }
    }
}
