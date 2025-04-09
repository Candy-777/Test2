using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TokenManager
{
    public interface IJwtProvider
    {       
        Task<string> GenerateRefreshToken(User user);
        Task<string> GenerateAccessToken(User user);
        Task<bool> ValidateToken(string token);
    }
}
