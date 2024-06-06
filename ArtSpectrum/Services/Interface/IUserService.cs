using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;

namespace ArtSpectrum.Services.Interface
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken);
        Task<List<UserDto>> GetAll();


        Task<UserDto> GetUserByIdAsync(int userId, CancellationToken cancellationToken);
        Task<UserDto> UpdateUserAsync(int userId, UpdateUserRequest request, CancellationToken cancellationToken);
        Task<UserDto> DeleteUserByIdAsync(int userId, CancellationToken cancellationToken);

    }
}
