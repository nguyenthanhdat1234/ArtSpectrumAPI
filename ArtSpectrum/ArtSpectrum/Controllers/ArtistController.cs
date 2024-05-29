using ArtSpectrum.Commons;
using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Filters.ActionFilters;
using ArtSpectrum.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArtSpectrum.Controllers
{
    public class ArtistController : ApiControllerBase
    {
        private readonly IArtistService _service;

        public ArtistController(IArtistService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<ArtistDto>>>> GetAllArtist()
        {
            var result = await _service.GetAll();
            return Ok(Result<List<ArtistDto>>.Succeed(result));
        }
        [HttpGet("{artistId:int}")]
        public async Task<ActionResult<Result<ArtistDto>>> GetArtistById([FromRoute] int artistId)
        {
            var result = await _service.GetArtistByIdAsync(artistId, new CancellationToken());
            return Ok(Result<ArtistDto>.Succeed(result));
        }

        [HttpPost]
        /*[ValidateRequest(typeof(CreateUserRequest))]*/
        public async Task<ActionResult<Result<ArtistDto>>> CreateArtist([FromBody] CreateArtistRequest request)
        {
            var result = await _service.CreateArtistAsync(request, new CancellationToken());
            return Ok(Result<ArtistDto>.Succeed(result));
        }

        [HttpPut("{artistId:int}")]
        /*[ValidateRequest(typeof(UpdateUserRequest))]*/
        public async Task<ActionResult<Result<ArtistDto>>> UpdateArtist(int artistId, UpdateArtistRequest request)
        {
            var result = await _service.UpdateArtistAsync(artistId, request, new CancellationToken());
            return Ok(Result<ArtistDto>.Succeed(result));
        }

        [HttpDelete("{artistId:int}")]
        public async Task<ActionResult<Result<ArtistDto>>> DeleteArtistById([FromRoute] int artistId)
        {
            var result = await _service.DeleteArtistByIdAsync(artistId, new CancellationToken());
            return Ok(Result<ArtistDto>.Succeed(result));
        }
    }
}
