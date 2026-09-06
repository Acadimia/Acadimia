// CompleteWithdrawalDto.cs
using System.ComponentModel.DataAnnotations;

namespace Acadimia.Infrastructure.Dtos.Wallet
{
    // بعد ما يتم التحويل البنكي فعلياً، يتم تأكيده هون فيخصم من الرصيد
    public class CompleteWithdrawalDto
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        public string TransferReference { get; set; }
    }
}