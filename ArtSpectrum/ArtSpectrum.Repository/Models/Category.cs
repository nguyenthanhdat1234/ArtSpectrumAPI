using System;
using System.Collections.Generic;

namespace ArtSpectrum.Repository.Models
{
    public partial class Category
    {
        public Category()
        {
            Paintings = new HashSet<Painting>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Painting> Paintings { get; set; }
    }
}
