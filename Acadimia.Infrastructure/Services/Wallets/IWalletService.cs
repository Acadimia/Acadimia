using Acadimia.Data.Models;
using Acadimia.Infrastructure.Dtos;
using Acadimia.Infrastructure.Dtos.Wallet;

namespace Acadimia.Infrastructure.Services.Wallets
{
    public interface IWalletService
    {
        Task<OperationResult> CreateWalletForUserAsync(string userId);
        Task<WalletDto> GetMyWalletAsync(string userId);
        Task<decimal> GetBalanceAsync(string userId);
        Task<PagedResultDto<List<WalletTransactionDto>>> GetTransactionHistoryAsync(string userId, DataTableRequestDto request);
        Task<OperationResult> SubmitTopUpRequestAsync(string userId, decimal amount, string bankReferenceNo, string receiptFileUrl);
        Task<OperationResult> SubmitWithdrawalRequestAsync(string userId, WithdrawalRequestInputDto input);
        Task<List<WalletTopUpRequest>> GetPendingTopUpRequestsAsync();
        Task<List<WithdrawalRequest>> GetPendingWithdrawalRequestsAsync();
        Task<OperationResult> VerifyTopUpRequestAsync(string adminId, VerifyTopUpRequestDto input);
        Task<OperationResult> DecideWithdrawalRequestAsync(string adminId, WithdrawalDecisionDto input);
        Task<OperationResult> CompleteWithdrawalAsync(string adminId, CompleteWithdrawalDto input);
    }
}