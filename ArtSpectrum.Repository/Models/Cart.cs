using System;
using System.Collections.Generic;

namespace ArtSpectrum.Repository.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int PaintingId { get; set; }
        public int Quantity { get; set; }

        public virtual Painting Painting { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
