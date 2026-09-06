using Acadimia.Core.Enums;
using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos;
using Acadimia.Infrastructure.Dtos.Wallet;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Acadimia.Infrastructure.Services.Wallets
{
    public class WalletService : IWalletService
    {
        private readonly ApplicationDbContext _context;

        // Whitelist لتفادي مشكلة Swagger اللي بيبعت "string" كقيمة افتراضية لـ SortColumn/SortColumnDirection
        private static readonly HashSet<string> AllowedSortColumns = new(StringComparer.OrdinalIgnoreCase)
        {
            "Id", "Amount", "Direction", "Type", "Status", "CreatedOn"
        };
        private static readonly HashSet<string> AllowedSortDirections = new(StringComparer.OrdinalIgnoreCase)
        {
            "asc", "desc"
        };

        public WalletService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==================== Wallet lifecycle ====================

        public async Task<OperationResult> CreateWalletForUserAsync(string userId)
        {
            var result = new OperationResult();

            var existingWallet = await _context.Wallets
                .IgnoreQueryFilters() // احتياطاً لو كانت متحذوفة سوفت-ديليت
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (existingWallet != null)
            {
                result.Success = true;
                result.Message = Messages.Success;
                return result;
            }

            try
            {
                var wallet = new Wallet
                {
                    UserId = userId,
                    Balance = 0,
                    CreatedBy = userId,
                    CreatedOn = DateTime.Now
                };

                await _context.Wallets.AddAsync(wallet);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = Messages.Success;
            }
            catch (Exception)
            {
                result.Message = Messages.Failed;
            }

            return result;
        }

        public async Task<WalletDto> GetMyWalletAsync(string userId)
        {
            var wallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null)
                return new WalletDto { UserId = userId, Balance = 0 };

            return new WalletDto
            {
                Id = wallet.Id,
                UserId = wallet.UserId,
                Balance = wallet.Balance
            };
        }

        public async Task<decimal> GetBalanceAsync(string userId)
        {
            var wallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == userId);
            return wallet?.Balance ?? 0;
        }

        // ==================== Transaction history ====================

        public async Task<PagedResultDto<List<WalletTransactionDto>>> GetTransactionHistoryAsync(string userId, DataTableRequestDto request)
        {
            var wallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null)
                return new PagedResultDto<List<WalletTransactionDto>> { Data = new List<WalletTransactionDto>(), TotalCount = 0 };

            IQueryable<WalletTransaction> transactions = _context.WalletTransactions
                .Where(t => t.WalletId == wallet.Id);

            if (!string.IsNullOrEmpty(request.SortColumn) && !string.IsNullOrEmpty(request.SortColumnDirection)
                && AllowedSortColumns.Contains(request.SortColumn) && AllowedSortDirections.Contains(request.SortColumnDirection))
            {
                transactions = transactions.OrderBy(string.Concat(request.SortColumn, " ", request.SortColumnDirection));
            }
            else
            {
                transactions = transactions.OrderByDescending(t => t.CreatedOn);
            }

            var totalCount = await transactions.CountAsync();

            var data = await transactions.Skip(request.Skip).Take(request.PageSize)
                .Select(t => new WalletTransactionDto
                {
                    Id = t.Id,
                    WalletId = t.WalletId,
                    Direction = t.Direction,
                    Type = t.Type,
                    Amount = t.Amount,
                    Status = t.Status,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn
                }).ToListAsync();

            return new PagedResultDto<List<WalletTransactionDto>> { Data = data, TotalCount = totalCount };
        }

        // ==================== Student flow: Top-Up ====================

        public async Task<OperationResult> SubmitTopUpRequestAsync(string userId, decimal amount, string bankReferenceNo, string receiptFileUrl)
        {
            var result = new OperationResult();
            try
            {
                var wallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == userId);
                if (wallet == null)
                {
                    result.Message = Messages.Failed;
                    return result;
                }

                var topUpRequest = new WalletTopUpRequest
                {
                    StudentId = userId,
                    Amount = amount,
                    BankReferenceNo = bankReferenceNo,
                    ReceiptFileUrl = receiptFileUrl,
                    Status = TopUpRequestStatus.PendingVerification,
                    CreatedBy = userId,
                    CreatedOn = DateTime.Now
                };
                await _context.WalletTopUpRequests.AddAsync(topUpRequest);
                await _context.SaveChangesAsync();

                // إنشاء حركة معلّقة فور تقديم الطلب - Pending لحين مراجعة الأدمن للصورة
                var transaction = new WalletTransaction
                {
                    WalletId = wallet.Id,
                    Direction = WalletTransactionDirection.In,
                    Type = WalletTransactionType.TopUp,
                    Amount = amount,
                    Status = WalletTransactionStatus.Pending,
                    Description = $"طلب شحن رصيد - مرجع البنك: {bankReferenceNo}",
                    RelatedEntityType = nameof(WalletTopUpRequest),
                    RelatedEntityId = topUpRequest.Id,
                    CreatedBy = userId,
                    CreatedOn = DateTime.Now
                };
                await _context.WalletTransactions.AddAsync(transaction);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = Messages.Success;
                result.ReturnId = topUpRequest.Id;
            }
            catch (Exception)
            {
                result.Message = Messages.Failed;
            }
            return result;
        }

        // ==================== Instructor flow: Withdrawal ====================

        public async Task<OperationResult> SubmitWithdrawalRequestAsync(string userId, WithdrawalRequestInputDto input)
        {
            var result = new OperationResult();
            try
            {
                var wallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == userId);
                if (wallet == null)
                {
                    result.Message = Messages.Failed;
                    return result;
                }

                if (wallet.Balance < input.Amount)
                {
                    // ملاحظة: يفضّل إضافة مفتاح مخصص "InsufficientBalance" في Messages.resx لاحقاً
                    result.Message = Messages.Failed;
                    return result;
                }

                var withdrawalRequest = new WithdrawalRequest
                {
                    InstructorId = userId,
                    Amount = input.Amount,
                    BankIBAN = input.BankIBAN,
                    BankName = input.BankName,
                    AccountHolderName = input.AccountHolderName,
                    Status = WithdrawalRequestStatus.PendingApproval,
                    CreatedBy = userId,
                    CreatedOn = DateTime.Now
                };
                await _context.WithdrawalRequests.AddAsync(withdrawalRequest);
                await _context.SaveChangesAsync();

                var transaction = new WalletTransaction
                {
                    WalletId = wallet.Id,
                    Direction = WalletTransactionDirection.Out,
                    Type = WalletTransactionType.Withdrawal,
                    Amount = input.Amount,
                    Status = WalletTransactionStatus.Pending,
                    Description = $"طلب سحب رصيد إلى {input.BankName}",
                    RelatedEntityType = nameof(WithdrawalRequest),
                    RelatedEntityId = withdrawalRequest.Id,
                    CreatedBy = userId,
                    CreatedOn = DateTime.Now
                };
                await _context.WalletTransactions.AddAsync(transaction);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = Messages.Success;
                result.ReturnId = withdrawalRequest.Id;
            }
            catch (Exception)
            {
                result.Message = Messages.Failed;
            }
            return result;
        }

        // ==================== Admin/Finance: Pending lists ====================

        public async Task<List<WalletTopUpRequest>> GetPendingTopUpRequestsAsync()
        {
            return await _context.WalletTopUpRequests
                .Where(r => r.Status == TopUpRequestStatus.PendingVerification)
                .OrderBy(r => r.CreatedOn)
                .ToListAsync();
        }

        public async Task<List<WithdrawalRequest>> GetPendingWithdrawalRequestsAsync()
        {
            return await _context.WithdrawalRequests
                .Where(r => r.Status == WithdrawalRequestStatus.PendingApproval)
                .OrderBy(r => r.CreatedOn)
                .ToListAsync();
        }

        // ==================== Admin/Finance: Top-Up decision ====================

        public async Task<OperationResult> VerifyTopUpRequestAsync(string adminId, VerifyTopUpRequestDto input)
        {
            var result = new OperationResult();

            var request = await _context.WalletTopUpRequests.SingleOrDefaultAsync(r => r.Id == input.RequestId);
            if (request == null || request.Status != TopUpRequestStatus.PendingVerification)
            {
                result.Message = Messages.Failed;
                return result;
            }

            var wallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == request.StudentId);
            if (wallet == null)
            {
                result.Message = Messages.Failed;
                return result;
            }

            var relatedTransaction = await _context.WalletTransactions
                .FirstOrDefaultAsync(t => t.RelatedEntityType == nameof(WalletTopUpRequest) && t.RelatedEntityId == request.Id);

            try
            {
                if (input.Approve)
                {
                    // بعد مراجعة صورة الإشعار والتأكد من التحويل: اعتماد الطلب وشحن الرصيد
                    wallet.Balance += request.Amount;
                    request.Status = TopUpRequestStatus.Completed;

                    if (relatedTransaction != null)
                        relatedTransaction.Status = WalletTransactionStatus.Completed;

                    _context.Wallets.Update(wallet);
                }
                else
                {
                    request.Status = TopUpRequestStatus.Rejected;
                    request.RejectionReason = input.RejectionReason;

                    if (relatedTransaction != null)
                        relatedTransaction.Status = WalletTransactionStatus.Rejected;
                }

                request.VerifiedBy = adminId;
                request.VerifiedOn = DateTime.Now;
                _context.WalletTopUpRequests.Update(request);

                if (relatedTransaction != null)
                {
                    relatedTransaction.DecisionBy = adminId;
                    relatedTransaction.DecisionOn = DateTime.Now;
                    _context.WalletTransactions.Update(relatedTransaction);
                }

                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = Messages.Success;
            }
            catch (Exception)
            {
                result.Message = Messages.Failed;
            }

            return result;
        }

        // ==================== Admin/Finance: Withdrawal decision ====================

        public async Task<OperationResult> DecideWithdrawalRequestAsync(string adminId, WithdrawalDecisionDto input)
        {
            var result = new OperationResult();

            var request = await _context.WithdrawalRequests.SingleOrDefaultAsync(r => r.Id == input.RequestId);
            if (request == null || request.Status != WithdrawalRequestStatus.PendingApproval)
            {
                result.Message = Messages.Failed;
                return result;
            }

            var relatedTransaction = await _context.WalletTransactions
                .FirstOrDefaultAsync(t => t.RelatedEntityType == nameof(WithdrawalRequest) && t.RelatedEntityId == request.Id);

            try
            {
                if (input.Approve)
                {
                    // موافقة فقط - المبلغ يُحجز، لا يُخصم إلا عند تأكيد التحويل الفعلي (CompleteWithdrawal)
                    request.Status = WithdrawalRequestStatus.ApprovedPendingTransfer;

                    if (relatedTransaction != null)
                        relatedTransaction.Status = WalletTransactionStatus.Accepted;
                }
                else
                {
                    request.Status = WithdrawalRequestStatus.Rejected;
                    request.RejectionReason = input.RejectionReason;

                    if (relatedTransaction != null)
                        relatedTransaction.Status = WalletTransactionStatus.Rejected;
                }

                request.ApprovedBy = adminId;
                _context.WithdrawalRequests.Update(request);

                if (relatedTransaction != null)
                {
                    relatedTransaction.DecisionBy = adminId;
                    relatedTransaction.DecisionOn = DateTime.Now;
                    _context.WalletTransactions.Update(relatedTransaction);
                }

                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = Messages.Success;
            }
            catch (Exception)
            {
                result.Message = Messages.Failed;
            }

            return result;
        }

        // ==================== Admin/Finance: Confirm bank transfer completed ====================

        public async Task<OperationResult> CompleteWithdrawalAsync(string adminId, CompleteWithdrawalDto input)
        {
            var result = new OperationResult();

            var request = await _context.WithdrawalRequests.SingleOrDefaultAsync(r => r.Id == input.RequestId);
            if (request == null || request.Status != WithdrawalRequestStatus.ApprovedPendingTransfer)
            {
                result.Message = Messages.Failed;
                return result;
            }

            var wallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == request.InstructorId);
            if (wallet == null || wallet.Balance < request.Amount)
            {
                result.Message = Messages.Failed;
                return result;
            }

            var relatedTransaction = await _context.WalletTransactions
                .FirstOrDefaultAsync(t => t.RelatedEntityType == nameof(WithdrawalRequest) && t.RelatedEntityId == request.Id);

            try
            {
                wallet.Balance -= request.Amount;
                request.Status = WithdrawalRequestStatus.Completed;
                request.TransferReference = input.TransferReference;

                _context.Wallets.Update(wallet);
                _context.WithdrawalRequests.Update(request);

                if (relatedTransaction != null)
                {
                    relatedTransaction.Status = WalletTransactionStatus.Completed;
                    relatedTransaction.DecisionBy = adminId;
                    relatedTransaction.DecisionOn = DateTime.Now;
                    _context.WalletTransactions.Update(relatedTransaction);
                }

                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = Messages.Success;
            }
            catch (Exception)
            {
                result.Message = Messages.Failed;
            }

            return result;
        }
    }
}