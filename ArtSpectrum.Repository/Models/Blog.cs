using System;
using System.Collections.Generic;

namespace ArtSpectrum.Repository.Models
{
    public partial class Blog
    {
        public int BlogId { get; set; }
        public string TiteBlog { get; set; } = null!;
        public string DescriptionBlog { get; set; } = null!;
        public string ImgBlog { get; set; } = null!;
        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; } = null!;
    }
}
