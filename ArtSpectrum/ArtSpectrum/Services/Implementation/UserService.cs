using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Exceptions;
using ArtSpectrum.Models;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Services.Interface;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ArtSpectrum.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        private readonly IValidator<CreateUserRequest> _addUserValidator;
        private readonly IValidator<UpdateUserRequest> _updateUserValidator;


        public UserService(IUnitOfWork uow, IMapper mapper, IValidator<CreateUserRequest> addUserValidator, IValidator<UpdateUserRequest> updateUserValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _addUserValidator = addUserValidator;
            _updateUserValidator = updateUserValidator;
        }
        public async Task<UserDto> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.FirstOrDefaultAsync(x =>
                x.Username.Trim().ToLower().Equals(request.Username.Trim().ToLower())
                || x.Email.Trim().Equals(request.Email.Trim()),
            cancellationToken);

            if(user is not null)
            {
                throw new Exception("This username/email has already been taken.");
            }

            var userEntity = new User()
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                FullName = request.FullName,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Role = request.Role,
            };
            var result = await _uow.UserRepository.AddAsync(userEntity);

            await _uow.Commit(cancellationToken);
            return _mapper.Map<UserDto>(result);

        }

        public async Task<UserDto> DeleteUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
            if (user is null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            else
            {
                _uow.UserRepository.Delete(user);
                await _uow.Commit(cancellationToken);
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetAll()
        {
            var result = await _uow.UserRepository.GetAll();
            return _mapper.Map<List<UserDto>>(result);
        }

        public async Task<UserDto> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            var result = await _uow.UserRepository.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if (result is null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> UpdateUserAsync(int userId, UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var validation = await _updateUserValidator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new RequestValidationException(validation.Errors);
            }
            var user = await _uow.UserRepository.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            if(user is null)
            {
                throw new KeyNotFoundException("User is not found.");
            }

            user.Username = request.Username.Trim();
            user.Password = request.Password;
            user.Email = request.Email.Trim();
            user.Address = request.Address?.Trim();
            user.PhoneNumber = request.PhoneNumber?.Trim();
            user.FullName = request.FullName?.Trim();

            _uow.UserRepository.Update(user);
            await _uow.Commit(cancellationToken);

            return _mapper.Map<UserDto>(user);

        }
    }
}
