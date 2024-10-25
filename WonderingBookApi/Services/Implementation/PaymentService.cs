using Microsoft.EntityFrameworkCore;
using WonderingBookApi.Data;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWalletService _walletService;

        public PaymentService(ApplicationDbContext context, IWalletService walletService)
        {
            _context = context;
            _walletService = walletService;
        }
        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            try
            {
                payment.PaymentId = Guid.NewGuid();
                payment.CreatedAt = DateTime.Now;
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                var wallet = await _walletService.UpdateWalletAsync(payment.WalletId, payment.PayAmount);
                return payment;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating wallet: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Payment>> GetPaymentHistoryOfWallet(int walletId)
        {
            return await _context.Payments.Where(p => p.WalletId == walletId).ToListAsync();
        }
    }
}
