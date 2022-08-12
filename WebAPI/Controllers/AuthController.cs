using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core;
using WebAPI.Data;
using WebAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly UserManager<WebApiUser> _userManager;
        private readonly SignInManager<WebApiUser> _signInManager;

        public AuthController(IConfiguration configuration,
            SignInManager<WebApiUser> signInManager,
            UserManager<WebApiUser> userManager)
        {
            _config = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromForm]DtoWebApiUser loginModel)
        {
            IActionResult result = Unauthorized();
            if (await _userManager.FindByNameAsync(loginModel.Username) is not { } user) return result;
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
            if (signInResult.Succeeded)
            {
                result = Ok(await BuildJwtToken(user.Id));
            }
            return result;
        }

        [WebApiAuthorize]
        public async Task<DtoWebApiResponse> TestAuthorize()
        {
            return new DtoWebApiResponse { Data = await Task.Run(() => "Authorize, Eureka!") };
        }

        [AllowAnonymous]
        public async Task<DtoWebApiResponse> TestAnonymous()
        {
            return new DtoWebApiResponse { Data = await Task.Run(() => "Anonymous, Eureka!") };
        }

#if DEBUG
        [AllowAnonymous]
        public async Task<DtoWebApiResponse> CreateDefaultUser()
        {
            var identityResult = await _userManager.CreateAsync(new WebApiUser() { UserName = "admin", Email = "test@example.com" }, "admin");
            var result = identityResult.Succeeded ?
                "Default User Created, Eureka!" :
                @$"Error! Code: { identityResult.Errors.FirstOrDefault()?.Code }, 
                   Description: {identityResult.Errors.FirstOrDefault()?.Description }";

            return new DtoWebApiResponse()
            {
                Data = result
            };
        }
#endif
        private async Task<string> BuildJwtToken(string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = CurrentUser;
            }

            var token = await Task.Run(() =>
            {
                var claims = new List<Claim>
                {
            new(JwtRegisteredClaimNames.Sub, userId)
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                return new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            });
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
