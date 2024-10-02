using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WonderingBookApi.DTOs;
using WonderingBookApi.Models;

namespace WonderingBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<User> _roleManager;
        private readonly IConfiguration _configuration;
        public UserController(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(RegisterUserDTO user)
        {
            string message = "";
            IdentityResult result = new();

            try
            {
                User u = new User()
                {
                    Fullname = user.Fullname,
                    Email = user.Email,
                    UserName = user.UserName
                };
                result = await _userManager.CreateAsync(u, user.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result);
                }
                message = "Registered Successfully";
                return Ok(new { message = message, token = GenerateJwtToken(u) });
            }
            catch(Exception ex)
            {
                return BadRequest("Something went wrong. Try again. " + ex.Message);
            }
            
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(LoginUserDTO login)
        {
            string message = "";

            try
            {
                User u = await _userManager.FindByEmailAsync(login.Email);
                if(u == null)
                {
                    return Unauthorized("Check your login credentials. Incorrect email or password");
                }
                
                if(u != null && !u.EmailConfirmed)
                {
                    //Development only. Add a method to confirm user email
                    u.EmailConfirmed = true;
                }
                
                var result = await _signInManager.PasswordSignInAsync(u, login.Password, login.IsRemember, false);

                if (!result.Succeeded)
                {
                    return Unauthorized("Check your login credentials. Incorrect email or password");
                }

                u.LastActiveAt = DateTime.Now;
                var updatedResult = await _userManager.UpdateAsync(u);
                
                message = "Login Successfully";
                return Ok(new { message = message, token = GenerateJwtToken(u) });
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong. Try again. " + ex.Message);
            }
        }

        [HttpGet("logout"), Authorize]
        public async Task<ActionResult> LogoutUser()
        {
            string message = "You are free to go!";
            try
            {
                await _signInManager.SignOutAsync();
            }catch(Exception ex)
            {
                return BadRequest("Something went wrong. Try again. " + ex.Message);
            }

            return Ok( new { message = message});
        }

        [HttpGet("admin"), Authorize]
        public ActionResult AdminPage()
        {
            string[] partners = { "Tran Hoang Giang", "Tran Duc Hung" };
            return Ok(new { trustedPartners = partners });
        }

        [HttpGet("home/{userId}"), Authorize]
        public async Task<ActionResult> HomePage(string userId)
        {
            User userInfo = await _userManager.FindByIdAsync(userId);
            if(userInfo == null)
            {
                return BadRequest(new { message = "Something went wrong. Try again." });
            }
            return Ok(new { userInfo = userInfo });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(14400),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
