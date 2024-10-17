using Microsoft.EntityFrameworkCore;
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

        public Task<FinancialTransaction> GetTransactionByCodeAsync(string code)
        {
            if (code == null)
                throw new ArgumentNullException(nameof(code));
            var transaction = _context.FinancialTransactions.FirstOrDefaultAsync(t => t.TransactionCode == code);
            return transaction;
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

        public async Task UpdateTransactionAsync(FinancialTransaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));
            try
            {
                _context.Entry(transaction).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}
