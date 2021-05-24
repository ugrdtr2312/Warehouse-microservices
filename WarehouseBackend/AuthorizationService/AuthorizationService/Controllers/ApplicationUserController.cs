using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthorizationService.Handlers;
using AuthorizationService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ApplicationUserController> _logger;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, ILogger<ApplicationUserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Register
        public async Task<object> PostApplicationUser(ApplicationUserModel model)
        {
            model.Role = "User";
            var applicationUser = new ApplicationUser() {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _userManager.AddToRoleAsync(applicationUser, model.Role);
                DateTime localDate = DateTime.Now;
                _logger.LogInformation($"/api/ApplicationUser/Register executed at {localDate} registred user {applicationUser.FullName}");
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/ApplicationUser/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return BadRequest(new {message = "Username or password is incorrect."});
            
            //Get role assigned to the user
            var role = await _userManager.GetRolesAsync(user);
            var options = new IdentityOptions();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserID",user.Id),
                    new Claim(options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault() ?? string.Empty)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtService.Token)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            DateTime localDate = DateTime.Now;
            _logger.LogInformation($"/api/ApplicationUser/Login executed at {localDate} logged in user {user.FullName}");
            return Ok(new { token });
        }
    }
}