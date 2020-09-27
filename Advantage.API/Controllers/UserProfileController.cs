using System;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Database.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        #region Private properties
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Constructor
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        #endregion

        #region Routes
        // GET api/userprofile
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.UserName
            };
        }

        // GET api/userprofile/admin
        [HttpGet]
        [Authorize(Roles="Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("ForAdmin")]
        public string GetForAdmin()
        {
            return "Web method for Admin role";
        }

        [HttpGet]
        [Authorize(Roles = "Customer", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("ForCustomer")]
        public string GetForCustomer()
        {
            return "Web method for Customer role";
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Customer", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("ForAdminOrCustomer")]
        public string GetForAdminOrCustomer()
        {
            return "Web method for Admin or Customer role";
        }
        #endregion
    }
}