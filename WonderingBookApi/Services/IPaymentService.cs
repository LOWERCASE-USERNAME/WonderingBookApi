using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IPaymentService
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
        //Task<Payment> UpdatePaymentAsync();
        //Task<Payment> GetPaymentAsync();
        //Task<IEnumerable<Payment>> GetUserPaymentAsync(string userId);
    }
}
