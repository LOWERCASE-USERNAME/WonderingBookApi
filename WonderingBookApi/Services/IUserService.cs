using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(string id);

        Task UpdateUser(User user);
    }
}
