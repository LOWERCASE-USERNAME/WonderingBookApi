using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WonderingBookApi.Utilities;

namespace WonderingBookApi.Models
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; }
        [Required]
        public int WalletId { get; set; }
        [Required]
        [Precision(18,2)]
        public decimal PayAmount { get; set; }
        [Required]
        public PaymentType? PaymentType { get; set; }
        [Required]
        public DateTime? CreatedAt { get; set; }

        public virtual Wallet? Wallet { get; set; }

    }
}
