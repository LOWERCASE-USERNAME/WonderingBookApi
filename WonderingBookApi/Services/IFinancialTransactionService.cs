using WonderingBookApi.Models;

namespace WonderingBookApi.Services
{
    public interface IFinancialTransactionService
    {
        // Use to create new Transaction 
        Task<FinancialTransaction> CreateTransactionAsync(FinancialTransaction transaction);
        Task<FinancialTransaction> GetArticleByIdAsync();
        Task<IEnumerable<FinancialTransaction>> GetAllTransactionsAsync();
        Task UpdateTransactionAsync();
        Task<IEnumerable<FinancialTransaction>> GetUserSuccessTransactionsAsync();

    }
}
