using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WonderingBookApi.Models
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        [Precision(18, 2)]
        public decimal? Balance { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
        public virtual ICollection<FinancialTransaction>? Transactions { get; set; }
    }
}
