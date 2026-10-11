namespace Acadimia.Infrastructure.Dtos.Wallet
{
    public class WalletDto
    {

        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Balance { get; set; }
        public decimal ReservedBalance { get; set; }   // pending withdrawals
        public decimal AvailableBalance { get; set; }  // Balance - ReservedBalance
    }
}