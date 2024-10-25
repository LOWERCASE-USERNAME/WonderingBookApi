using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensions.Msal;
using WonderingBookApi.DTOs.Article;
using WonderingBookApi.DTOs.FinancialTransaction;
using WonderingBookApi.Models;
using WonderingBookApi.Services;
using WonderingBookApi.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WonderingBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinacialTransactionController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IFinancialTransactionService _transact;
        private readonly IWalletService _wallet;
        private readonly IPaymentService _payment;
        private readonly IMapper _mapper;
        public FinacialTransactionController(
            IMapper mapper, 
            IFinancialTransactionService transact, 
            IWalletService wallet, 
            IPaymentService payment,
            IConfiguration config)
        {
            _mapper = mapper;
            _transact = transact;
            _wallet = wallet;
            _payment = payment;
            _config = config;
        }
        // GET: api/<FinacialTransactionController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FinacialTransactionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FinacialTransactionController>
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromForm] CreateTransactionDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // Find Wallet if no wallet
            var wallet = await _wallet.GetUserWalletAsync(request.UserId);
            // Create transaction
            var transaction = new FinancialTransaction
            {
                Amount = request.Amount,
                WalletId = wallet.WalletId,
                Status = TransactionStatus.Pending
            };
            var createdTransaction = await _transact.CreateTransactionAsync(transaction);
            var bankId = _config["VietQrQuickLink:BANK_ID"];
            var accountNo = _config["VietQrQuickLink:ACCOUNT_NO"];
            var template = _config["VietQrQuickLink:TEMPLATE"];
            var amount = createdTransaction.Amount;
            var description = createdTransaction.TransactionCode;
            var account = _config["VietQrQuickLink:ACCOUNT_NAME"];
            // return QR code
            return Ok($"https://img.vietqr.io/image/{bankId}-{accountNo}-{template}.png?amount={amount}&addInfo={description}&accountName={account}");
        }

        // POST api/<FinacialTransactionController>/
        [HttpPost("check")]
        public async Task<IActionResult> TransactionCheck([FromBody] CheckTransactionDTO transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                // Find transaction
                var existTransaction = await _transact.GetTransactionByCodeAsync(transaction.TransactionCode);
                // Check Amount
                if(existTransaction.Amount == transaction.Amount && transaction.TransactionDate <= existTransaction.ExpiredAt)
                {
                    existTransaction.Status = TransactionStatus.Success;
                }
                // Update Status
                await _transact.UpdateTransactionAsync(existTransaction);
                var wallet = await _wallet.UpdateWalletAsync(existTransaction.WalletId, existTransaction.Amount);
                return Ok(existTransaction);
                // return transaction
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
        }

        [HttpGet("get-personal-wallet")]
        public async Task<IActionResult> GetPersonalWallet(Guid userId)
        {
            var wallet = await _wallet.GetUserWalletAsync(userId.ToString());
            return Ok(wallet);
        }

        [HttpGet("get-payment-history")]
        public async Task<IActionResult> GetPaymentHistory(int walletId)
        {
            var wallet = await _payment.GetPaymentHistoryOfWallet(walletId);
            return Ok(wallet);
        }

        // DELETE api/<FinacialTransactionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
