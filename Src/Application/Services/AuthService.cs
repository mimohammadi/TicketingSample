using Application.Configurations;
using Application.Dtos.Auth;
using Application.Helpers;
using Application.Interfaces;
using Application.Mappers;
using Domain;
using Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IRepository<User> _userRepository;

        public AuthService(JwtSettings jwtSettings, IRepository<User> userRepository)
        {
            _jwtSettings = jwtSettings;
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByPrediction(u => u.Email == dto.Email);
            if (user == null || user == default)
                throw new ArgumentNullException("User not found");

            if (!PasswordHelper.Verify(user.Password, dto.Password))
                throw new ArgumentException("User or Password not match");

            return new LoginResponseDto
            {
                Token = GenerateJwtToken(user)
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.Expiration),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
