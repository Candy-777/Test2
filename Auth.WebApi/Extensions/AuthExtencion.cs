using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, string secretKey)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;            
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.HttpContext.Request.Cookies["access_token"];
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                            //Console.WriteLine($"Token found: {token}");
                        }                  

                        return Task.CompletedTask;
                    },
                    //OnTokenValidated = async context =>
                    //{
                    //    //Console.WriteLine("Validated");
                    //},
                    //OnAuthenticationFailed = context =>
                    //{
                    //    Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                    //    return Task.CompletedTask;
                    //},
                    //OnChallenge = context =>
                    //{
                    //    Console.WriteLine("Challenge triggered.");
                    //    return Task.CompletedTask;
                    //}
                };
            });



            return services;
        }
    }
}