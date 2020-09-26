using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Advantage.API.Database.Entities;
using Advantage.API.Models;
using Advantage.API.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Private properties
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly ApplicationSettings _appSettings;

        #endregion

        #region Constructor
        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signinManager,
            IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _appSettings = appSettings.Value;
        }

        #endregion

        #region Routes
        // POST api/user/register
        [HttpPost]
        [Route("Register")]
        public async Task<Object> CreateUser(UserDetailModel newUser)
        {
            var applicationUser = new ApplicationUser()
            {
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                UserName = newUser.UserName
            };

            var result = await _userManager.CreateAsync(applicationUser, newUser.Password);
            return Ok(result);
        }

        // POST api/user/login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var passwordIsValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (user != null && passwordIsValid)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] 
                    { 
                        new Claim("UserId", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_appSettings.JWT_Secret.ToString())),
                        SecurityAlgorithms.HmacSha256Signature
                    ),
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
        }

        #endregion
    }
}