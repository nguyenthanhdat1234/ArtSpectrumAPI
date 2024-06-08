using ArtSpectrum.Contracts.Request;
using ArtSpectrum.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ArtSpectrum.Utils
{

    public class GenerateJSONWebTokenHelper
    {
        private readonly IConfiguration _configuration;
        public GenerateJSONWebTokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJSONWebToken(LoginRequest loginDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
