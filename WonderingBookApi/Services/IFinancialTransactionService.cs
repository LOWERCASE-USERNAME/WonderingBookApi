using WonderingBookApi.Models;
using WonderingBookApi.Utilities;

namespace WonderingBookApi.Services
{
    public interface IFinancialTransactionService
    {
        // Use to create new Transaction 
        Task<FinancialTransaction> CreateTransactionAsync(FinancialTransaction transaction);
        Task<FinancialTransaction> GetTransactionByCodeAsync(string code);
        // Task<IEnumerable<FinancialTransaction>> GetAllTransactionsAsync();
        Task UpdateTransactionAsync(FinancialTransaction transaction);
        // Task<IEnumerable<FinancialTransaction>> GetUserSuccessTransactionsAsync();

    }
}
