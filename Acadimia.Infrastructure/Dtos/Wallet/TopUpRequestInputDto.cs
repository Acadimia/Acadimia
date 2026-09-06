using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Acadimia.Infrastructure.Dtos.Wallet
{
    public class TopUpRequestInputDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string BankReferenceNo { get; set; }

        [Required]
        public IFormFile ReceiptFile { get; set; }
    }
}