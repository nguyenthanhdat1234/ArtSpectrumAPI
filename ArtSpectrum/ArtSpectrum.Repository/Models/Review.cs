using System;
using System.Collections.Generic;

namespace ArtSpectrum.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int PaintingId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public virtual Painting Painting { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
