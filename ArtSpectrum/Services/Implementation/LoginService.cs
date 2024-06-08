using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Exceptions;
using ArtSpectrum.Filters.ActionFilters;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Services.Interface;
using ArtSpectrum.Utils;
using AutoMapper;
using System.Net.NetworkInformation;

namespace ArtSpectrum.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public LoginService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<UserDto> Authenticate(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.FirstOrDefaultAsync(x => x.Email == request.Email 
            && x.Password == request.Password, cancellationToken);

            if(user == null)
            {
                throw new ConflictException("User not found!");
            }

            return _mapper.Map<UserDto>(user);
        }   
    }
}
