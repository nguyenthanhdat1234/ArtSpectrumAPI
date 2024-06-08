using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;

namespace ArtSpectrum.Services.Interface
{
    public interface ILoginService
    {
        Task<UserDto> Authenticate(LoginRequest loginRequest, CancellationToken cancellationToken);
    }
}
