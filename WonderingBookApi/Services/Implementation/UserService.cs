using Microsoft.AspNetCore.Identity;
using WonderingBookApi.Data;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public UserService(ApplicationDbContext context) 
        {
            _context = context;
        }
        public Task<IEnumerable<User>> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
