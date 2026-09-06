using System.ComponentModel.DataAnnotations;

namespace Acadimia.Infrastructure.Dtos.Wallet
{
    public class WithdrawalRequestInputDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string BankIBAN { get; set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        public string AccountHolderName { get; set; }
    }
}