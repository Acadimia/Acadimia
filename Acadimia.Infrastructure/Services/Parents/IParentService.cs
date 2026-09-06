using Acadimia.Data.Models;
using Acadimia.Infrastructure.Dtos.Parent;

namespace Acadimia.Infrastructure.Services.Parent
{
    public interface IParentService
    {
        Task<List<ChildOverviewDto>> GetMyChildrenAsync(string parentUserId);
        Task<List<ChildAttendanceRowDto>> GetChildAttendanceAsync(string parentUserId, int studentId, DateTime? from, DateTime? to);
        Task<List<ChildExamResultRowDto>> GetChildExamResultsAsync(string parentUserId, int studentId);
        Task<OperationResult> LinkChildAsync(string parentUserId, int studentId, Constant relation);
    }
}