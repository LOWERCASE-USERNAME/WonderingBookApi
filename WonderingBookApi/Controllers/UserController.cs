using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WonderingBookApi.DTOs;
using WonderingBookApi.Models;
using WonderingBookApi.Services;
using WonderingBookApi.Utilities;

namespace WonderingBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<User> _roleManager;
        private readonly IConfiguration _configuration;
        public UserController(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration, IUserService userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _userService = userService;
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
                    UserName = user.UserName,
                    Status = UserStatus.Active
                };
                result = await _userManager.CreateAsync(u, user.Password);
                await _userService.AssignRoleAsync(u,"RegularUser");

                if (!result.Succeeded)
                {
                    return BadRequest(result);
                }
                message = "Registered Successfully";
                return Ok(new { message = message, token = GenerateJwtToken(u, await _userManager.GetRolesAsync(u)) });
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
                // Check if the user is banned
                if (u.Status == UserStatus.Banned) return Unauthorized("Your account is banned. Please contact support.");

                var result = await _signInManager.PasswordSignInAsync(u, login.Password, login.IsRemember, false);

                if (!result.Succeeded)
                {
                    return Unauthorized("Check your login credentials. Incorrect email or password");
                }

                u.LastActiveAt = DateTime.Now;
                var updatedResult = await _userManager.UpdateAsync(u);
                message = "Login Successfully";
                return Ok(new { message = message, token = GenerateJwtToken(u, await _userManager.GetRolesAsync(u)) });
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong. Try again. " + ex.Message);
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] string token)
        {
            // Validate the Google token
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(token);
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid Google token. " + ex.Message);
            }

            // Check if the user already exists
            var user = await _userManager.FindByEmailAsync(payload.Email);

            if (user != null)
            {
                // Check if the user is banned
                if (user.Status == UserStatus.Banned) return Unauthorized("Your account is banned. Please contact support.");
                // Log the user in
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(new { message = "Login Successful", token = GenerateJwtToken(user, await _userManager.GetRolesAsync(user)) });
            }

            // If the user doesn't exist, create a new account
            var username = payload.Email.Substring(0, payload.Email.IndexOf('@'));
            var userToCreate = new User
            {
                Email = payload.Email,
                UserName = username,
                Fullname = payload.Name, // Assuming you want to store the full name from Google
                Status = UserStatus.Active
            };

            var createUserResult = await _userManager.CreateAsync(userToCreate);
            await _userService.AssignRoleAsync(userToCreate, "RegularUser");

            if (createUserResult.Succeeded)
            {
                await _signInManager.SignInAsync(userToCreate, isPersistent: false);
                return Ok(new { message = "User created and logged in successfully", token = GenerateJwtToken(userToCreate, await _userManager.GetRolesAsync(userToCreate)) });
            }

            return BadRequest(createUserResult.Errors.Select(e => e.Description));
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

        private string GenerateJwtToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

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

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // PUT: api/user/status/{userId}/{status}
        [HttpPut("status/{userId}/{status}")]
        public async Task<IActionResult> UpdateStatus(string userId, UserStatus status)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.Status = status;
            var result = await _userService.UpdateUserAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Status has been updated." });
        }


        // PUT: api/user/{userId}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute]string id, [FromBody] EditUserDTO userDTO)
        {
            if(id.IsNullOrEmpty())
            {
                return BadRequest("id must not empty");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return all validation errors
            }

            var user = await _userManager.FindByIdAsync(id);
            if(user == null) return NotFound();
            user.Fullname = userDTO.Fullname;
            user.Email = userDTO.Email;
            user.UserName = userDTO.UserName;
            user.Status = userDTO.Status;
            foreach(var role in userDTO.Roles)
            {
                await _userService.AssignRoleAsync(user, role);
            }

            var result = await _userService.UpdateUserAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "User has been updated." });
        }

    }
}
