using Core.Domain.Entities;
using Core.Interfaces;
using Infrastructure.Data.RoleManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TokenManager
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        private readonly RoleManager _roleManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public JwtProvider(IOptions<JwtOptions> options, RoleManager roleManager, IRefreshTokenRepository refreshTokenRepository)
        {
            _options = options.Value;
            _roleManager = roleManager;
            _refreshTokenRepository = refreshTokenRepository; 
        }
        public  async Task<string> GenerateAccessToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new("userId", user.Id.ToString()),

            };
            var roles = await _roleManager.GetUserRoles(user);
            foreach (var role in roles) 
            {
                var claim = new Claim(ClaimTypes.Role, role);
                claims.Add(claim);
            }

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenLifeTime)
                );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public async Task<string> GenerateRefreshToken(User user)
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            var token = Convert.ToBase64String(randomBytes);

            var refreshToken = new RefreshToken { Expires = DateTime.UtcNow.AddDays(_options.RefreshTokenLifeTime),User_Id = user.Id, Token = token };
            var result = await _refreshTokenRepository.Add(refreshToken);
            if (result > 0)
            {
                return token;
            }
            else
            {
                throw new Exception("Cant create RefreshToken");
            }
        }

        public Task<bool> ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var validTo = handler.ReadJwtToken(token).ValidTo;
            return Task.FromResult(validTo > DateTime.UtcNow);
        }
    }
}
