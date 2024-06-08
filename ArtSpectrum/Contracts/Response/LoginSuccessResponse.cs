using ArtSpectrum.DTOs;

namespace ArtSpectrum.Contracts.Response
{
    public class LoginSuccessResponse
    {
        public bool Success => true;
        public UserCredential? UserCredentials { get; set; }

        public string? AccessToken { get; set; }

    }

    public class UserCredential
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = null!;
    }
}
