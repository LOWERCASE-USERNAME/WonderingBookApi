using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IPaymentService
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentHistoryOfWallet(int walletId);
        //Task<Payment> UpdatePaymentAsync();
        //Task<Payment> GetPaymentAsync();
        //Task<IEnumerable<Payment>> GetUserPaymentAsync(string userId);
    }
}
