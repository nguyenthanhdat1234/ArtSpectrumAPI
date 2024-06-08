using System;
using System.Collections.Generic;

namespace ArtSpectrum.Repository.Models
{
    public partial class Artist
    {
        public Artist()
        {
            Blogs = new HashSet<Blog>();
        }

        public int ArtistId { get; set; }
        public int UserId { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
        public bool? Approved { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
