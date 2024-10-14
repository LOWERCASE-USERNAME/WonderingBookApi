using Microsoft.EntityFrameworkCore;
using WonderingBookApi.Data;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services.Implementation
{
    public class WalletService : IWalletService
    {
        private readonly ApplicationDbContext _context;

        public WalletService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Wallet> CreateWalletAsync(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException("UserId cannot be null or empty.");
                }

                var wallet = new Wallet
                {
                    UserId = userId,
                    Balance = 0,
                };
                await _context.Wallets.AddAsync(wallet);
                await _context.SaveChangesAsync();
                return wallet;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating wallet: {ex.Message}");
                throw;
            }
        }

        public async Task<Wallet> GetUserWalletAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("UserId cannot be null or empty.");
            }
            if (!_context.Wallets.Any(w => w.UserId == userId))
            {
                var wallet = await CreateWalletAsync(userId);
                return wallet;
            }
            return await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
        }
    }
}
