using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IWalletService
    {
        Task<Wallet> CreateWalletAsync(string userId);
        Task<Wallet> UpdateWalletAsync(int walletId, decimal amount);
        Task<Wallet> GetUserWalletAsync(string userId);
        Task<Wallet> GetWalletAsync(int walletId);

    }
}
