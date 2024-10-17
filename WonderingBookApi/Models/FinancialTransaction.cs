using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WonderingBookApi.Utilities;

namespace WonderingBookApi.Models
{
    public class FinancialTransaction
    {
        [Key]
        public Guid FinancialTransactionId { get; set; }
        [Required]
        public int WalletId { get; set; }
        [Required]
        public string TransactionCode { get; set; } = string.Empty;
        [Required]
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        [Required]
        public DateTime? CreatedAt { get; set; }
        [Required]
        public DateTime? ExpiredAt { get; set; }
        [Required]
        public TransactionStatus? Status { get; set; } 

        public virtual Wallet? Wallet { get; set; }
    }
}
