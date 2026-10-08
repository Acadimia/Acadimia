using Acadimia.Data.DbContext;
using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Auth;
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

        var unread = await _context.Notifications.CountAsync(n => n.UserId == parentUserId && !n.IsRead);

        var result = new List<ChildOverviewDto>();
        foreach (var link in links)
        {
            var studentId = link.StudentId;

            var totalSessions = await _context.Attendances.CountAsync(a => a.StudentId == studentId);
            var presentSessions = await _context.Attendances
                .CountAsync(a => a.StudentId == studentId && a.Status == AttendanceStatus.Present);

            // FR-P07: الاختبارات ذات TotalMarks = 0 لا تُحتسب (نفس منطق StudentService) لتفادي القسمة على صفر
            var scores = await _context.ExamResults
                .Where(r => r.StudentId == studentId && r.Exam.TotalMarks > 0)
                .Select(r => new { r.ScoreObtained, r.Exam.TotalMarks })
                .ToListAsync();

            result.Add(new ChildOverviewDto
            {
                StudentId = studentId,
                StudentName = link.Student.Name,
                GradeName = link.Student.Grade?.Name,
                AttendanceRatePercent = totalSessions == 0 ? 0 : Math.Round((decimal)presentSessions / totalSessions * 100, 1),
                AverageExamScorePercent = scores.Count == 0
                    ? (decimal?)null
                    : Math.Round(scores.Average(x => x.ScoreObtained / x.TotalMarks) * 100, 1),
                UnreadNotificationsCount = unread
            });
        }
        return result;
    }

    // كل ميثود متابعة تتحقق من ملكية الرابط أولًا
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

        var rows = await _context.ExamResults
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

        foreach (var row in rows)
            row.Percentage = row.TotalMarks > 0 ? Math.Round(row.ScoreObtained / row.TotalMarks * 100, 1) : (decimal?)null;

        return rows;
    }

    // ==================== FR-P09 / FR-P10 : إدارة الربط (Admin) ====================

    private Task<bool> IsValidRelationAsync(int relationTypeId) =>
        _context.Constants.AnyAsync(c => c.Id == relationTypeId && c.ParentId == (int)GeneralEnums.Kinship);

    private async Task ClearPrimaryAsync(int studentId, int? exceptLinkId, string? userId)
    {
        var others = await _context.ParentStudentLinks
            .Where(l => l.StudentId == studentId && l.IsPrimaryContact && l.Id != exceptLinkId)
            .ToListAsync();
        foreach (var o in others)
        {
            o.IsPrimaryContact = false;
            SetUpdatedFields(o, userId);
        }
    }

    public async Task<OperationResult> LinkChildAsync(string parentUserId, int studentId, int? relationTypeId, bool isPrimaryContact = false)
    {
        var result = new OperationResult(false, Messages.Invalid);

        var parentOk = await _context.Users.AnyAsync(u => u.Id == parentUserId
            && !u.IsDeleted && u.IsActive && u.UserTypeId == UserTypeIds.Parent);
        if (!parentOk) { result.Message = "المستخدم ليس ولي أمر فعّالًا"; return result; }

        if (!await _context.Students.AnyAsync(s => s.Id == studentId))
        { result.Message = "الطالب غير موجود"; return result; }

        if (relationTypeId != null && !await IsValidRelationAsync(relationTypeId.Value))
        { result.Message = "نوع صلة القرابة غير صالح"; return result; }

        var currentUserId = await GetCurrentUserIdAsync();

        // IgnoreQueryFilters: الفهرس الفريد (ParentUserId, StudentId) يشمل السجلات المحذوفة soft-delete
        var existing = await _context.ParentStudentLinks.IgnoreQueryFilters()
            .FirstOrDefaultAsync(l => l.ParentUserId == parentUserId && l.StudentId == studentId);

        if (existing != null && !existing.IsDeleted)
        {
            result.Message = "الطالب مرتبط بالفعل بهذا الحساب";
            return result;
        }

        try
        {
            if (isPrimaryContact) await ClearPrimaryAsync(studentId, existing?.Id, currentUserId);

            if (existing != null) // إعادة تفعيل رابط محذوف
            {
                existing.IsDeleted = false;
                existing.DeletedBy = null;
                existing.RelationTypeId = relationTypeId;
                existing.IsPrimaryContact = isPrimaryContact;
                SetUpdatedFields(existing, currentUserId);
            }
            else
            {
                var link = new ParentStudentLink
                {
                    ParentUserId = parentUserId,
                    StudentId = studentId,
                    RelationTypeId = relationTypeId,
                    IsPrimaryContact = isPrimaryContact
                };
                SetCreatedFields(link, currentUserId);
                await _context.ParentStudentLinks.AddAsync(link);
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

    // FR-P10: تعديل صلة القرابة / جهة الاتصال الأساسية
    public async Task<OperationResult> UpdateRelationAsync(ParentLinkUpdateDto input)
    {
        var result = new OperationResult(false, Messages.Invalid);

        var link = await _context.ParentStudentLinks.SingleOrDefaultAsync(l => l.Id == input.LinkId);
        if (link == null) { result.Message = Messages.Failed; return result; }

        if (input.RelationTypeId != null && !await IsValidRelationAsync(input.RelationTypeId.Value))
        { result.Message = "نوع صلة القرابة غير صالح"; return result; }

        var currentUserId = await GetCurrentUserIdAsync();
        try
        {
            if (input.IsPrimaryContact) await ClearPrimaryAsync(link.StudentId, link.Id, currentUserId);

            link.RelationTypeId = input.RelationTypeId;
            link.IsPrimaryContact = input.IsPrimaryContact;
            SetUpdatedFields(link, currentUserId);

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

    public async Task<OperationResult> UnlinkAsync(int linkId)
    {
        var result = new OperationResult(false, Messages.Failed);

        var link = await _context.ParentStudentLinks.SingleOrDefaultAsync(l => l.Id == linkId);
        if (link == null) return result;

        link.IsDeleted = true;
        link.DeletedBy = await GetCurrentUserIdAsync();
        link.IsPrimaryContact = false;
        await _context.SaveChangesAsync();

        result.Success = true;
        result.Message = Messages.Success;
        return result;
    }

    public async Task<List<ParentLinkDto>> GetLinksAsync(int? studentId, string? parentUserId)
    {
        var q = _context.ParentStudentLinks.AsQueryable();
        if (studentId.HasValue) q = q.Where(l => l.StudentId == studentId.Value);
        if (!string.IsNullOrEmpty(parentUserId)) q = q.Where(l => l.ParentUserId == parentUserId);

        return await q.OrderByDescending(l => l.CreatedOn)
            .Select(l => new ParentLinkDto
            {
                Id = l.Id,
                ParentUserId = l.ParentUserId,
                ParentName = l.ParentUser.Name,
                StudentId = l.StudentId,
                StudentName = l.Student.Name,
                RelationTypeId = l.RelationTypeId,
                RelationTypeName = l.RelationType != null ? l.RelationType.Name : null,
                IsPrimaryContact = l.IsPrimaryContact
            }).ToListAsync();
    }

    public async Task<List<LookupItemDto>> GetRelationTypesAsync() =>
        await _context.Constants.Where(c => c.ParentId == (int)GeneralEnums.Kinship)
            .Select(c => new LookupItemDto { Id = c.Id, Name = c.Name }).ToListAsync();
}