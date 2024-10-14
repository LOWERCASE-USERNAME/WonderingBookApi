using WonderingBookApi.Data;
using WonderingBookApi.Models;

namespace WonderingBookApi.Services.Implementation
{
    public class FinancialTransactionService : IFinancialTransactionService
    {
        private readonly ApplicationDbContext _context;

        public FinancialTransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FinancialTransaction> CreateTransactionAsync(FinancialTransaction transaction)
        {
            transaction.FinancialTransactionId = Guid.NewGuid();
            transaction.CreatedAt = DateTime.UtcNow;
            transaction.ExpiredAt = DateTime.UtcNow.AddMinutes(10);
            transaction.TransactionCode = TransactionCodeGenerate();
            
            _context.FinancialTransactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public Task<IEnumerable<FinancialTransaction>> GetAllTransactionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FinancialTransaction> GetArticleByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FinancialTransaction>> GetUserSuccessTransactionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public string TransactionCodeGenerate()
        {
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var code = new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            var result = code + timestamp;
            return result;
        }
    }
}
