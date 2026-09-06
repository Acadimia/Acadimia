using Acadimia.Core.Enums;

namespace Acadimia.Infrastructure.Dtos.Wallet
{
    // Flat DTO - لا يوجد Navigation properties عشان نتفادى مشاكل الـ binding
    public class WalletTransactionDto
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public WalletTransactionDirection Direction { get; set; }
        public WalletTransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public WalletTransactionStatus Status { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}