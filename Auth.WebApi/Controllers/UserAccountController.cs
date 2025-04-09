using Application.DTO.Requests;
using Application.Services;
using Infrastructure.Data.RoleManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Auth.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly UserAccountService _userAccountService;
        private readonly RoleManager _roleManager;

        public UserAccountController(UserAccountService userAccountService, RoleManager roleManager)
        {
            _userAccountService = userAccountService;
            _roleManager = roleManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser(string username, string password)
        {
            var request = new RegisterRequest { Password = password, Username = username };

            var result = await _userAccountService.Register(request);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var request = new LoginRequest { Password = password, Username = username };

            var result = await _userAccountService.Login(request);

            Response.Cookies.Append("access_token", result.AccessToken,new CookieOptions { HttpOnly = true, Secure = true});
            return Ok(result);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string name)
        {
            var result = await _roleManager.CreateNewRole(name);
            return Ok(result);
        }

        [HttpPost("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(string name, string role)
        {
            var result = await _userAccountService.AddRoleToUser(name, role);
            return Ok(result);
        }


        [HttpGet]
        [Authorize (Roles = "Yarik")]
        public async Task<IActionResult> Test()
        {
            return Ok("Test");
        }
    }
}
