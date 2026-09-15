// Acadimia.Infrastructure/Services/Parent/ParentService.cs
using Acadimia.Data.Enums;
using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Parent;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Parent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class ParentService : BaseService, IParentService
{
    public ParentService(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        : base(context, userManager, httpContextAccessor) { }

    public async Task<List<ChildOverviewDto>> GetMyChildrenAsync(string parentUserId)
    {
        var links = await _context.ParentStudentLinks
            .Where(l => l.ParentUserId == parentUserId)
            .Include(l => l.Student).ThenInclude(s => s.Grade)
            .ToListAsync();

        var result = new List<ChildOverviewDto>();
        foreach (var link in links)
        {
            var studentId = link.StudentId;

            var totalSessions = await _context.Attendances.CountAsync(a => a.StudentId == studentId);
            var presentSessions = await _context.Attendances
                .CountAsync(a => a.StudentId == studentId && a.Status == AttendanceStatus.Present);

            var examResults = await _context.ExamResults
                .Include(r => r.Exam)
                .Where(r => r.StudentId == studentId)
                .ToListAsync();

            result.Add(new ChildOverviewDto
            {
                StudentId = studentId,
                StudentName = link.Student.Name,
                GradeName = link.Student.Grade?.Name,
                AttendanceRatePercent = totalSessions == 0 ? 0 : Math.Round((decimal)presentSessions / totalSessions * 100, 1),
                AverageExamScorePercent = examResults.Count == 0
                    ? null
                    : Math.Round(examResults.Average(r => r.ScoreObtained / r.Exam.TotalMarks) * 100, 1),
                UnreadNotificationsCount = await _context.Notifications
                    .CountAsync(n => n.UserId == parentUserId && !n.IsRead)
            });
        }
        return result;
    }

    // كل ميثود متابعة تتحقق من ملكية الرابط أولًا — هاد أهم سطر أمني بكل الميزة
    private Task<bool> HasAccessAsync(string parentUserId, int studentId) =>
        _context.ParentStudentLinks.AnyAsync(l => l.ParentUserId == parentUserId && l.StudentId == studentId);

    public async Task<List<ChildAttendanceRowDto>> GetChildAttendanceAsync(string parentUserId, int studentId, DateTime? from, DateTime? to)
    {
        if (!await HasAccessAsync(parentUserId, studentId)) return new List<ChildAttendanceRowDto>();

        var query = _context.Attendances.Include(a => a.Group).Where(a => a.StudentId == studentId);
        if (from.HasValue) query = query.Where(a => a.SessionDate >= from.Value);
        if (to.HasValue) query = query.Where(a => a.SessionDate <= to.Value);

        return await query.OrderByDescending(a => a.SessionDate)
            .Select(a => new ChildAttendanceRowDto
            {
                SessionDate = a.SessionDate,
                GroupName = a.Group.Name,
                Status = a.Status,
                Notes = a.Notes
            }).ToListAsync();
    }

    public async Task<List<ChildExamResultRowDto>> GetChildExamResultsAsync(string parentUserId, int studentId)
    {
        if (!await HasAccessAsync(parentUserId, studentId)) return new List<ChildExamResultRowDto>();

        return await _context.ExamResults.Include(r => r.Exam)
            .Where(r => r.StudentId == studentId)
            .OrderByDescending(r => r.Exam.ExamDate)
            .Select(r => new ChildExamResultRowDto
            {
                ExamTitle = r.Exam.Title,
                ExamDate = r.Exam.ExamDate,
                ScoreObtained = r.ScoreObtained,
                TotalMarks = r.Exam.TotalMarks,
                Feedback = r.Feedback
            }).ToListAsync();
    }

    public async Task<OperationResult> LinkChildAsync(string parentUserId, int studentId, Constant relation)
    {
        var result = new OperationResult();
        if (await HasAccessAsync(parentUserId, studentId))
        {
            result.Message = "الطالب مرتبط بالفعل بهذا الحساب";
            return result;
        }

        var currentUserId = await GetCurrentUserIdAsync(); 
        var link = new ParentStudentLink { ParentUserId = parentUserId, StudentId = studentId, RelationType = relation };
        SetCreatedFields(link, currentUserId);

        await _context.ParentStudentLinks.AddAsync(link);
        await _context.SaveChangesAsync();

        result.Success = true;
        result.Message = Messages.Success;
        return result;
    }
}