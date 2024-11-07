using Microsoft.AspNetCore.Identity;
using WonderingBookApi.DTOs;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ListUserDTO>> GetAllUsersAsync();
        Task<EditUserDTO> GetUserByIdAsync(string userId);
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<IdentityResult> AssignRoleAsync(User user, string role);
    }
}
