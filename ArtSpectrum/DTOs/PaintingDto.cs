﻿using ArtSpectrum.Mappings;
using ArtSpectrum.Repository.Models;

namespace ArtSpectrum.DTOs
{
    public class PaintingDto : IMapFrom<Painting>
    {
        public int PaintingId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public double? SalesPrice { get; set; }
        public int SaleId { get; set; }
        public int ArtistsId { get; set; }
        public string? FullName { get; set; }


    }
}
