using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using ArtSpectrum.Exceptions;
using ArtSpectrum.Repository.Models;
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
            // Kiểm tra xem username hoặc email đã tồn tại trong hệ thống chưa
            var existingUser = await _uow.UserRepository.FirstOrDefaultAsync(x =>
                x.Username.Trim().ToLower() == request.Username.Trim().ToLower()
                || x.Email.Trim() == request.Email.Trim(),
                cancellationToken);

            if (existingUser != null)
            {
                throw new Exception("This username/email has already been taken.");
            }

            // Kiểm tra role có phải là "buyer" hoặc "artist"
            string checkRole = request.Role.Trim().ToLower();
            if (checkRole != "buyer" && checkRole != "artist")
            {
                throw new Exception("User must choose role BUYER or ARTIST");
            }

            // Tạo entity User mới
            var userEntity = new User()
            {
                Username = request.Username.Trim(),
                Password = request.Password, // Chú ý: Hãy xem xét mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
                Email = request.Email.Trim(),
                FullName = request.FullName.Trim(),
                Address = request.Address?.Trim(), // Đảm bảo kiểm tra null trước khi trim
                PhoneNumber = request.PhoneNumber?.Trim(), // Đảm bảo kiểm tra null trước khi trim
                Role = checkRole, // Lưu role đã kiểm tra
            };

            var addedUser = await _uow.UserRepository.AddAsync(userEntity);
            await _uow.Commit(cancellationToken);

            if (checkRole == "artist")
            {
                var artist = new Artist()
                {
                    UserId = addedUser.UserId, 
                    Bio = "", 
                    ProfilePicture = "",
                    Approved = true, 
                };

                await _uow.ArtistsRepository.AddAsync(artist);
                await _uow.Commit(cancellationToken);
            }

            
            

            
            return _mapper.Map<UserDto>(addedUser);
        }


        public async Task<UserDto> DeleteUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
            if (user is null)
            {
                throw new KeyNotFoundException("User not found.");
            } else if(user.Role.ToLower() == "admin")
            {
                throw new KeyNotFoundException("This account not delete.");
            }

            var carts = await _uow.CartRepository.WhereAsync(x => x.UserId == user.UserId, cancellationToken);
            if (carts.Any())
            {
                foreach (var cart in carts)
                {
                    _uow.CartRepository.Delete(cart);
                }
            }

            var artist = await _uow.ArtistsRepository.FirstOrDefaultAsync(x => x.UserId == user.UserId, cancellationToken);
            if (artist != null)
            {
                _uow.ArtistsRepository.Delete(artist);

                var blogs = await _uow.BlogRepository.WhereAsync(x => x.ArtistId == artist.ArtistId, cancellationToken);
                if (blogs.Any())
                {
                    foreach (var blog in blogs)
                    {
                        _uow.BlogRepository.Delete(blog);
                    }
                }
            }

            var reviews = await _uow.ReviewRepository.WhereAsync(x => x.UserId == user.UserId, cancellationToken);
            if (reviews.Any())
            {
                foreach (var review in reviews)
                {
                    _uow.ReviewRepository.Delete(review);
                }
            }

            _uow.UserRepository.Delete(user);

            await _uow.Commit(cancellationToken);

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
            string checkrole = request.Role.Trim().ToLower();
            if (checkrole != "buyer" && checkrole != "artist")
            {
                throw new Exception("User must choose role BUYER or ARTIST");
            }
            user.Username = request.Username.Trim();
            user.Password = request.Password;
            user.Email = request.Email.Trim();
            user.Address = request.Address?.Trim();
            user.PhoneNumber = request.PhoneNumber?.Trim();
            user.FullName = request.FullName?.Trim();
            user.Role = request.Role.Trim();

            _uow.UserRepository.Update(user);
            await _uow.Commit(cancellationToken);

            return _mapper.Map<UserDto>(user);

        }

        public async Task AddSampleUsersAsync(CancellationToken cancellationToken)
        {
            var sampleUsers = new List<CreateUserRequest>();
            var roles = new[] { "BUYER", "ARTIST" };

            for (int i = 1; i <= 100; i++)
            {
                var request = new CreateUserRequest
                {
                    Username = $"user{i}",
                    Password = $"password{i}",
                    Email = $"user{i}@example.com",
                    FullName = $"User {i}",
                    Address = $"Address {i}",
                    PhoneNumber = $"{i:0000000000}",
                    Role = roles[i % 2] // Alternates between "BUYER" and "ARTIST"
                };
                sampleUsers.Add(request);
            }

            foreach (var userRequest in sampleUsers)
            {
                try
                {
                    await CreateUserAsync(userRequest, cancellationToken);
                    Console.WriteLine($"User {userRequest.Username} created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating user {userRequest.Username}: {ex.Message}");
                    // Log errors if necessary
                }
            }
        }

    }


}
