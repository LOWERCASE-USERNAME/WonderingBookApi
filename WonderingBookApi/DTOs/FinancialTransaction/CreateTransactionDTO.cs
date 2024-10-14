using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WonderingBookApi.Models;

namespace WonderingBookApi.DTOs.FinancialTransaction

{
    public class CreateTransactionDTO
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal? Amount { get; set; }
    }
}
