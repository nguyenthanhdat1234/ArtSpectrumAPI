using System;
using System.Collections.Generic;

namespace ArtSpectrum.Repository.Models
{
    public partial class User
    {
        public User()
        {
            Artists = new HashSet<Artist>();
            Carts = new HashSet<Cart>();
            Reviews = new HashSet<Review>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = null!;

        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
