using Acadimia.Infrastructure.Dtos.Auth;
using Acadimia.Infrastructure.Dtos.Parent;

namespace Acadimia.Infrastructure.Services.Parent
{
    public interface IParentService
    {
        Task<List<ChildOverviewDto>> GetMyChildrenAsync(string parentUserId);
        Task<List<ChildAttendanceRowDto>> GetChildAttendanceAsync(string parentUserId, int studentId, DateTime? from, DateTime? to);
        Task<List<ChildExamResultRowDto>> GetChildExamResultsAsync(string parentUserId, int studentId);

        // FR-P09 / FR-P10 (للأدمن)
        Task<OperationResult> LinkChildAsync(string parentUserId, int studentId, int? relationTypeId, bool isPrimaryContact = false);
        Task<OperationResult> UpdateRelationAsync(ParentLinkUpdateDto input);
        Task<OperationResult> UnlinkAsync(int linkId);
        Task<List<ParentLinkDto>> GetLinksAsync(int? studentId, string? parentUserId);
        Task<List<LookupItemDto>> GetRelationTypesAsync();
    }
}