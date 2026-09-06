using Acadimia.Data.Models;

    namespace Acadimia.Infrastructure.Services.Wallets
    {
        public interface IWalletService
        {
            Task<OperationResult> CreateWalletForUserAsync(string userId);
            Task<Wallet> GetByUserIdAsync(string userId);
            Task<decimal> GetBalanceAsync(string userId);
        }
    }