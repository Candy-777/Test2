using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Responces;
using Core.Domain.Entities;
using Application.DTO.Requests;
using Infrastructure.PasswordHasher;
using System.Security.Cryptography.X509Certificates;
using Infrastructure.TokenManager;
using Infrastructure.Data.RoleManager;

namespace Application.Services
{
    public class UserAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly RoleManager _roleManager;

        public UserAccountService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, RoleManager roleManager)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _roleManager = roleManager;
        }

        public async Task<RegisterResponce> Register (RegisterRequest dto)
        {
            if (await _userRepository.GetByUsername(dto.Username) != null) 
            {
                throw new Exception("User Already exist");
            }
            var user = new User { Username = dto.Username, PasswordHash = _passwordHasher.GetHashPassword(dto.Password), Department = "УИС" };
            
            var result = await _userRepository.Add(user);

            return new RegisterResponce { UserId = result };            
        }

        public async Task<LoginResponce> Login (LoginRequest dto)
        {
            var user = await _userRepository.GetByUsername(dto.Username);
            if (user == null || !_passwordHasher.VerifyPassword(user,dto.Password) ) 
            {
                throw new Exception("Invalid username or password");
            }
            var token = await _jwtProvider.GenerateAccessToken(user);
            var refreshToken = await _jwtProvider.GenerateRefreshToken(user);

            return new LoginResponce { AccessToken = token , RefreshToken = refreshToken};
        }

        public async Task<bool> AddRoleToUser(string userName,string roleName)
        {
            var user = await _userRepository.GetByUsername(userName);
            if (user == null)
            {
                throw new Exception("Fail");
            }

            var result = await _roleManager.AddRoleToUser(user, roleName);
            return result;
        }
    }
}
