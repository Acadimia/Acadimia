using Acadimia.Infrastructure.Dtos;
using Acadimia.Infrastructure.Dtos.Commission;

namespace Acadimia.Infrastructure.Services.PlatformCommission
{
    public interface IPlatformCommissionService
    {
        Task<CurrentCommissionDto> GetCurrentAsync();
        Task<PagedResultDto<List<CommissionSettingDto>>> GetHistoryAsync(DataTableRequestDto request);
        Task<OperationResult> SetCommissionAsync(string adminId, SetCommissionInputDto input);
    }
}