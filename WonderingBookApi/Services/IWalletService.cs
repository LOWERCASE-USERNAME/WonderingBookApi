using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IWalletService
    {
        Task<Wallet> CreateWalletAsync(string userId);
        //Task<Wallet> UpdateWalletAsync();
        Task<Wallet> GetUserWalletAsync(string userId);
    }
}
