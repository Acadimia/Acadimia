using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos;
using Acadimia.Infrastructure.Dtos.Commission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Acadimia.Infrastructure.Services.PlatformCommission
{
    public class PlatformCommissionService : BaseService, IPlatformCommissionService
    {
        // نفس القيمة الاحتياطية المستخدمة في BookingService و JoinRequestService
        private const decimal DefaultCommissionPercentage = 10m;

        public PlatformCommissionService(ApplicationDbContext context, UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
            : base(context, userManager, httpContextAccessor)
        {
        }

        public async Task<CurrentCommissionDto> GetCurrentAsync()
        {
            var active = await _context.PlatformCommissionSettings
                .Where(s => s.IsActive)
                .OrderByDescending(s => s.EffectiveFrom)
                .Select(s => new { s.CommissionPercentage, s.EffectiveFrom })
                .FirstOrDefaultAsync();

            return active == null
                ? new CurrentCommissionDto { CommissionPercentage = DefaultCommissionPercentage, IsDefault = true }
                : new CurrentCommissionDto
                {
                    CommissionPercentage = active.CommissionPercentage,
                    IsDefault = false,
                    EffectiveFrom = active.EffectiveFrom
                };
        }

        public async Task<PagedResultDto<List<CommissionSettingDto>>> GetHistoryAsync(DataTableRequestDto request)
        {
            var query = _context.PlatformCommissionSettings.OrderByDescending(s => s.EffectiveFrom);

            var pageSize = Math.Clamp(request.PageSize, 1, 100);
            var skip = Math.Max(request.Skip, 0);

            var rows = await query.Skip(skip).Take(pageSize)
                .Select(s => new
                {
                    s.Id,
                    s.CommissionPercentage,
                    s.EffectiveFrom,
                    s.EffectiveTo,
                    s.IsActive,
                    s.CreatedBy
                }).ToListAsync();

            // أسماء الأدمن الذين حددوا النسب (CreatedBy = UserId)
            var adminIds = rows.Where(r => r.CreatedBy != null).Select(r => r.CreatedBy!).Distinct().ToList();
            var names = await _context.Users.Where(u => adminIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.Name);

            return new PagedResultDto<List<CommissionSettingDto>>
            {
                TotalCount = await query.CountAsync(),
                Data = rows.Select(r => new CommissionSettingDto
                {
                    Id = r.Id,
                    CommissionPercentage = r.CommissionPercentage,
                    EffectiveFrom = r.EffectiveFrom,
                    EffectiveTo = r.EffectiveTo,
                    IsActive = r.IsActive,
                    SetBy = r.CreatedBy != null && names.TryGetValue(r.CreatedBy, out var n) ? n : null
                }).ToList()
            };
        }

        public async Task<OperationResult> SetCommissionAsync(string adminId, SetCommissionInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            // decimal(5,2): نقرّب لخانتين عشريتين ونتحقق من النطاق
            var percentage = Math.Round(input.CommissionPercentage, 2);
            if (percentage < 0 || percentage > 100) return result;

            var strategy = _context.Database.CreateExecutionStrategy();
            try
            {
                return await strategy.ExecuteAsync(async () =>
                {
                    _context.ChangeTracker.Clear();
                    var attempt = new OperationResult(false, Messages.Invalid);

                    await using var transaction = await _context.Database.BeginTransactionAsync();

                    var now = DateTime.Now;
                    var actives = await _context.PlatformCommissionSettings.Where(s => s.IsActive).ToListAsync();

                    if (actives.Count == 1 && actives[0].CommissionPercentage == percentage)
                    {
                        attempt.Message = "هذه هي النسبة الحالية بالفعل";
                        return attempt;
                    }

                    var oldRate = actives.OrderByDescending(s => s.EffectiveFrom)
                        .Select(s => (decimal?)s.CommissionPercentage).FirstOrDefault();

                    // إغلاق كل السجلات الفعّالة (يضمن وجود سجل واحد فعّال فقط)
                    foreach (var old in actives)
                    {
                        old.IsActive = false;
                        old.EffectiveTo = now;
                        SetUpdatedFields(old, adminId);
                    }

                    var setting = new PlatformCommissionSetting
                    {
                        CommissionPercentage = percentage,
                        EffectiveFrom = now,
                        IsActive = true
                    };
                    SetCreatedFields(setting, adminId);
                    await _context.PlatformCommissionSettings.AddAsync(setting);
                    await _context.SaveChangesAsync(); // للحصول على setting.Id

                    // تدقيق: AuditLog (NFR - تغييرات حساسة)
                    await _context.AuditLogs.AddAsync(new AuditLog
                    {
                        UserId = adminId,
                        Action = "UpdateCommission",
                        EntityName = nameof(PlatformCommissionSetting),
                        EntityId = setting.Id.ToString(),
                        OldValues = oldRate == null ? null : JsonSerializer.Serialize(new { CommissionPercentage = oldRate }),
                        NewValues = JsonSerializer.Serialize(new { CommissionPercentage = percentage }),
                        CreatedBy = adminId,
                        CreatedOn = now
                    });

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    attempt.Success = true;
                    attempt.Message = Messages.Success;
                    attempt.ReturnId = setting.Id;
                    return attempt;
                });
            }
            catch (Exception)
            {
                result.Message = Messages.Failed;
                return result;
            }
        }
    }
}