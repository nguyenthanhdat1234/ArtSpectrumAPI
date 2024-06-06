using System;
using System.Collections.Generic;

namespace ArtSpectrum.Repository.Models
{
    public partial class Category
    {
        public Category()
        {
            PaintingCategories = new HashSet<PaintingCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<PaintingCategory> PaintingCategories { get; set; }
    }
}
