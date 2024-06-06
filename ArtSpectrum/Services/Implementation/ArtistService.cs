using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Exceptions;
using ArtSpectrum.Repository.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Services.Interface;
using AutoMapper;

namespace ArtSpectrum.Services.Implementation
{
    public class ArtistService : IArtistService
    {

        private readonly IUnitOfWork _uow;
        private IMapper _mapper;

        public ArtistService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ArtistDto> CreateArtistAsync(CreateArtistRequest request, CancellationToken cancellationToken)
        {
            var artist = await _uow.ArtistsRepository.FirstOrDefaultAsync(x =>
                x.UserId == request.UserId, cancellationToken);

            if (artist is not null)
            {
                throw new Exception("This artist has already been taken.");
            }

            var artistEntity = new Artist()
            {
                UserId = request.UserId,
                Bio = request.Bio,
                ProfilePicture = request.ProfilePicture,
                Approved = false,
            };
            var result = await _uow.ArtistsRepository.AddAsync(artistEntity);

            await _uow.Commit(cancellationToken);
            return _mapper.Map<ArtistDto>(result);
        }

        public async Task<ArtistDto> DeleteArtistByIdAsync(int artistId, CancellationToken cancellationToken)
        {
            var artist = await _uow.ArtistsRepository.FirstOrDefaultAsync(x => x.ArtistId == artistId, cancellationToken);
            if (artist is null)
            {
                throw new KeyNotFoundException("Artist not found.");
            }
            else
            {
                _uow.ArtistsRepository.Delete(artist);
                await _uow.Commit(cancellationToken);
            }
            return _mapper.Map<ArtistDto>(artist);
        }

        public async Task<List<ArtistDto>> GetAll()
        {
            var result = await _uow.ArtistsRepository.GetAll();
            return _mapper.Map<List<ArtistDto>>(result);
        }

        public async Task<ArtistDto> GetArtistByIdAsync(int artistId, CancellationToken cancellationToken)
        {
            var result = await _uow.ArtistsRepository.FirstOrDefaultAsync(x => x.ArtistId == artistId, cancellationToken);

            if (result is null)
            {
                throw new KeyNotFoundException("Artist not found.");
            }

            return _mapper.Map<ArtistDto>(result);
        }

        public async Task<ArtistDto> UpdateArtistAsync(int artistId, UpdateArtistRequest request, CancellationToken cancellationToken)
        {
            /*var validation = await _updateUserValidator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new RequestValidationException(validation.Errors);
            }*/
            var artist = await _uow.ArtistsRepository.FirstOrDefaultAsync(x => x.ArtistId == artistId, cancellationToken);

            if (artist is null)
            {
                throw new KeyNotFoundException("Artist is not found.");
            }

            artist.Bio = request.Bio.Trim();
            artist.ProfilePicture = request.ProfilePicture;



            _uow.ArtistsRepository.Update(artist);
            await _uow.Commit(cancellationToken);

            return _mapper.Map<ArtistDto>(artist);
        }
    }
}
