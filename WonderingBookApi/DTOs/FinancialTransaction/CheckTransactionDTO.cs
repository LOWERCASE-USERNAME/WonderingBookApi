using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WonderingBookApi.DTOs.FinancialTransaction
{
    public class CheckTransactionDTO
    {
        [Required]
        public string TransactionCode { get; set; } = string.Empty;
        [Required]
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
    }
}
