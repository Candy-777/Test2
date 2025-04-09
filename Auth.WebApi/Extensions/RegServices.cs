using Application.Services;
using Core.Interfaces;
using Infrastructure.Data.Repositories;
using Infrastructure.PasswordHasher;
using Infrastructure.TokenManager;
using Infrastructure.Data.RoleManager;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Extensions
{
    public static class RegServices
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services) 
        {
            services.AddScoped<IPasswordHasher,PassworHasher>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserAccountService>();
            services.AddScoped<RoleManager>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IRefreshTokenRepository,RefreshTokenRepository>();
            return services;
        }
    }
}
