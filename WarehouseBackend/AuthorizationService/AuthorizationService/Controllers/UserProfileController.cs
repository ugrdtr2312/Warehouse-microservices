using System.Linq;
using System.Threading.Tasks;
using AuthorizationService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        //GET : /api/UserProfile
        public async Task<object> GetUserProfile()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FullName,
                user.Email,
                user.UserName
            };
        }
        
        [HttpGet]
        [Authorize(Roles ="Admin")]
        [Route("Admin")]
        public string GetAdmin()
        {
            return "Admin";
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("User")]
        public string GetUser()
        {
            return "User";
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        [Route("InRole")]
        public string GetInRole()
        {
            return "In role";
        }
    }
}