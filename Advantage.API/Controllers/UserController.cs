using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Database.Entities;
using Advantage.API.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Private properties
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;

        #endregion

        #region Constructor
        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }

        #endregion

        #region Routes
        // GET api/user/register
        [HttpPost]
        [Route("Register")]
        public async Task<Object> CreateUser(UserDetailModel newUser)
        {
            var applicationUser = new ApplicationUser()
            {
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                UserName = (newUser.FirstName.Substring(0, 1) + newUser.LastName).ToLower()
            };

            var result = await _userManager.CreateAsync(applicationUser, newUser.Password);
            return Ok(result);
        }


        #endregion
    }
}