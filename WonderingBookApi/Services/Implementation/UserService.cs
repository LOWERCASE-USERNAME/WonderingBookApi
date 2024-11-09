using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WonderingBookApi.Data;
using WonderingBookApi.DTOs;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> AssignRoleAsync(User user, string role)
        {
            // Check if the role exists in the database
            if (!await _roleManager.RoleExistsAsync(role))
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Role '{role}' does not exist." });
            }

            return await _userManager.AddToRoleAsync(user, role);
        }


        public async Task<IEnumerable<ListUserDTO>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = new List<ListUserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new ListUserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Status = user.Status,
                    Fullname = user.Fullname,
                    Roles = roles.ToList()
                });
            }
            return userDtos;
        }

        public async Task<EditUserDTO> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u=> u.Id == userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return new EditUserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Status = user.Status,
                Fullname = user.Fullname,
                Roles = roles.ToList()
            };
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
