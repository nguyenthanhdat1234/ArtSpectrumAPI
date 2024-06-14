using ArtSpectrum.Attributes;
using ArtSpectrum.Commons;
using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Filters.ActionFilters;
using ArtSpectrum.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArtSpectrum.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _service;
        private IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [AdminAuthorize]
        public async Task<ActionResult<Result<List<UserDto>>>> GetAllUser()
        {
            var result = await _service.GetAll();
            return Ok(Result<List<UserDto>>.Succeed(result));
        }
        [HttpGet("{userId:int}")]
        public async Task<ActionResult<Result<UserDto>>> GetUserById([FromRoute] int userId)
        {
            var result = await _service.GetUserByIdAsync(userId, new CancellationToken());
            return Ok(Result<UserDto>.Succeed(result));
        }

        [HttpPost]
        [ValidateRequest(typeof(CreateUserRequest))]
        public async Task<ActionResult<Result<UserDto>>> CreateUser([FromBody] CreateUserRequest request)
        {
            var result = await _service.CreateUserAsync(request, new CancellationToken());
            return Ok(Result<UserDto>.Succeed(result));
        }

        [HttpPut("{userId:int}")]
        [ValidateRequest(typeof(UpdateUserRequest))]
        public async Task<ActionResult<Result<UserDto>>> UpdateUser(int userId, UpdateUserRequest request)
        {
            var result = await _service.UpdateUserAsync(userId, request, new CancellationToken());
            return Ok(Result<UserDto>.Succeed(result));
        }

        [HttpDelete("{userId:int}")]
        [AdminAuthorize]
        public async Task<ActionResult<Result<UserDto>>> DeleteUserById([FromRoute] int userId)
        {
            var result = await _service.DeleteUserByIdAsync(userId, new CancellationToken());
            return Ok(Result<UserDto>.Succeed(result));
        }

    }
}
