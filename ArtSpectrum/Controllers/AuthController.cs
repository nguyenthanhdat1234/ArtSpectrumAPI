using ArtSpectrum.Commons;
using ArtSpectrum.Contracts.Request;
using ArtSpectrum.Contracts.Response;
using ArtSpectrum.Services.Interface;
using ArtSpectrum.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace ArtSpectrum.Controllers
{
    public class AuthController : ApiControllerBase
    {
        public readonly ILoginService _service;
        private readonly GenerateJSONWebTokenHelper _helper;

        public AuthController(ILoginService service, GenerateJSONWebTokenHelper helper)
        {
            _service = service;
            _helper = helper;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _service.Authenticate(request, new CancellationToken());
            if(user != null)
            {
                var tokenString = _helper.GenerateJSONWebToken(user);
                var result = new LoginSuccessResponse
                {
                    UserCredentials = new UserCredential
                    { 
                        UserId = user.UserId,
                        Username = user.Username,
                        Email = user.Email,
                        FullName = user.FullName,
                        Address = user.Address,
                        PhoneNumber = user.PhoneNumber,
                        Role = user.Role,
                    },

                    AccessToken = tokenString,
                };

                return Ok(Result<LoginSuccessResponse>.Succeed(result));
            }
            else
            {
                return Unauthorized("Email or Password is incorrect!");
            }
        }
    }
}
