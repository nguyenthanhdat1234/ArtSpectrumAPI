using System;
using System.Collections.Generic;

namespace ArtSpectrum.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int PaintingId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtOrderTime { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Painting Painting { get; set; } = null!;
    }
}
