using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly UserManager<WebApiUser> _userManager = null;
        private readonly SignInManager<WebApiUser> _signInManager = null;

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
        public async Task<IActionResult> LogIn([FromForm]DTOWebApiUser loginModel)
        {
            IActionResult result = Unauthorized();
            WebApiUser _user = await _userManager.FindByNameAsync(loginModel.Username);
            if (_user != null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(_user, loginModel.Password, false);
                if (signInResult.Succeeded)
                {
                    result = Ok(BuildJwtToken(_user.Id));
                }
            }
            return result;
        }

        private string BuildJwtToken(string userId)
        {
            List<Claim> claims = new List<Claim>() {
            new Claim(JwtRegisteredClaimNames.Sub, userId)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
