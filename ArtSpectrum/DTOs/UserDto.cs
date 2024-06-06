using ArtSpectrum.Mappings;
using ArtSpectrum.Repository.Models;
using AutoMapper;

namespace ArtSpectrum.DTOs
{
    public class UserDto : IMapFrom<User>
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = null!;
    }
}
