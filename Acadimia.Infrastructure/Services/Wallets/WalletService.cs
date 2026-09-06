
    using Acadimia.Data.DbContext;
    using Acadimia.Data.Models;
    using Acadimia.Data.Resources;
    using Microsoft.EntityFrameworkCore;

    namespace Acadimia.Infrastructure.Services.Wallets
    {
        public class WalletService : IWalletService
        {
            private readonly ApplicationDbContext _context;

            public WalletService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<OperationResult> CreateWalletForUserAsync(string userId)
            {
                var result = new OperationResult();

                // تجنّب إنشاء أكثر من Wallet لنفس المستخدم (فيه Unique Index أصلاً بس منتحقق بدري)
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

            public async Task<Wallet> GetByUserIdAsync(string userId)
            {
                var wallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == userId);
                if (wallet != null)
                    return wallet;

                return new Wallet { UserId = userId, Balance = 0 };
            }

            public async Task<decimal> GetBalanceAsync(string userId)
            {
                var wallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == userId);
                return wallet?.Balance ?? 0;
            }
        }
    }