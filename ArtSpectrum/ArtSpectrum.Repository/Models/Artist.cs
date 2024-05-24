using System;
using System.Collections.Generic;

namespace ArtSpectrum.Repository.Models
{
    public partial class Artist
    {
        public int ArtistId { get; set; }
        public int UserId { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
        public bool? Approved { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
