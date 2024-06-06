using System;
using System.Collections.Generic;

namespace ArtSpectrum.Repository.Models
{
    public partial class PaintingCategory
    {
        public int PaintingCategoryId { get; set; }
        public int? PaintingId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Painting? Painting { get; set; }
    }
}
