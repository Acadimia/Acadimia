

using Acadimia.Data.DbContext;
using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Lessons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Infrastructure.Services.Lessons
{
    public class LessonService : BaseService, ILessonService
    {
        public LessonService(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
            : base(context, userManager, httpContextAccessor)
        {
        }

        private async Task<bool> HasConflictAsync(int? groupId, int? courseId, DateTime date, TimeSpan start, int durationMinutes, int? excludeLessonId)
        {
            var end = start.Add(TimeSpan.FromMinutes(durationMinutes));

            var query = _context.Lessons.Where(l => l.ScheduledDate.Date == date.Date
                                                   && l.Status != LessonStatus.Cancelled
                                                   && (excludeLessonId == null || l.Id != excludeLessonId));

            if (groupId != null)
                query = query.Where(l => l.GroupId == groupId);
            else if (courseId != null)
                query = query.Where(l => l.CourseId == courseId);

            var sameDayLessons = await query.ToListAsync();

            return sameDayLessons.Any(l =>
            {
                var existingEnd = l.StartTime.Add(TimeSpan.FromMinutes(l.DurationMinutes));
                return start < existingEnd && l.StartTime < end; 
            });
        }

        public async Task<OperationResult> CreateAsync(LessonInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            if (input.GroupId == null && input.CourseId == null)
            {
                result.Message = Messages.Invalid;
                return result;
            }

            if (await HasConflictAsync(input.GroupId, input.CourseId, input.ScheduledDate, input.StartTime, input.DurationMinutes, null))
            {
                result.Message = "يوجد تعارض مع حصة أخرى مجدولة في نفس الوقت"; // conflicts with another scheduled lesson
                return result;
            }

            var currentUserId = await GetCurrentUserIdAsync();
            var lastOrder = await _context.Lessons
                .Where(l => l.GroupId == input.GroupId && l.CourseId == input.CourseId)
                .MaxAsync(l => (int?)l.OrderIndex) ?? 0;

            var lesson = new Lesson
            {
                GroupId = input.GroupId,
                CourseId = input.CourseId,
                Title = input.Title,
                OrderIndex = input.OrderIndex > 0 ? input.OrderIndex : lastOrder + 1,
                ScheduledDate = input.ScheduledDate,
                StartTime = input.StartTime,
                DurationMinutes = input.DurationMinutes,
                Status = LessonStatus.Scheduled,
                MeetingPlatform = input.MeetingPlatform,
                MeetingUrl = input.MeetingUrl,
                MeetingInstructions = input.MeetingInstructions
            };
            SetCreatedFields(lesson, currentUserId);

            await _context.Lessons.AddAsync(lesson);
            await _context.SaveChangesAsync();

            result.Success = true;
            result.Message = Messages.Success;
            result.ReturnId = lesson.Id;
            return result;
        }

        public async Task<OperationResult> UpdateAsync(LessonInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var lesson = await _context.Lessons.SingleOrDefaultAsync(l => l.Id == input.Id);
            if (lesson == null)
            {
                result.Message = Messages.Failed;
                return result;
            }

            if (lesson.Status != LessonStatus.Scheduled || lesson.ScheduledDate < DateTime.Now.Date)
            {
                result.Message = Messages.Failed; 
                return result;
            }

            if (await HasConflictAsync(lesson.GroupId, lesson.CourseId, input.ScheduledDate, input.StartTime, input.DurationMinutes, lesson.Id))
            {
                result.Message = "يوجد تعارض مع حصة أخرى مجدولة في نفس الوقت";
                return result;
            }

            lesson.Title = input.Title;
            lesson.ScheduledDate = input.ScheduledDate;
            lesson.StartTime = input.StartTime;
            lesson.DurationMinutes = input.DurationMinutes;
            if (input.MeetingPlatform != null) lesson.MeetingPlatform = input.MeetingPlatform;
            if (input.MeetingUrl != null) lesson.MeetingUrl = input.MeetingUrl;
            if (input.MeetingInstructions != null) lesson.MeetingInstructions = input.MeetingInstructions;

            var currentUserId = await GetCurrentUserIdAsync();
            SetUpdatedFields(lesson, currentUserId);
            _context.Lessons.Update(lesson);
            SetEntityModifiedFields(lesson);
            await _context.SaveChangesAsync();

            result.Success = true;
            result.Message = Messages.Success;
            return result;
        }

        public async Task<OperationResult> ConfigureMeetingAsync(MeetingConfigInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var lesson = await _context.Lessons.SingleOrDefaultAsync(l => l.Id == input.LessonId);
            if (lesson == null)
            {
                result.Message = Messages.Failed;
                return result;
            }

            lesson.MeetingPlatform = input.MeetingPlatform;
            lesson.MeetingUrl = input.MeetingUrl;
            lesson.MeetingInstructions = input.MeetingInstructions;

            var currentUserId = await GetCurrentUserIdAsync();
            SetUpdatedFields(lesson, currentUserId);
            _context.Lessons.Update(lesson);
            SetEntityModifiedFields(lesson);
            await _context.SaveChangesAsync();

            result.Success = true;
            result.Message = Messages.Success;
            return result;
        }

        public async Task<OperationResult> CancelAsync(LessonCancelDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var lesson = await _context.Lessons.SingleOrDefaultAsync(l => l.Id == input.LessonId);
            if (lesson == null)
            {
                result.Message = Messages.Failed;
                return result;
            }

            if (lesson.Status != LessonStatus.Scheduled)
            {
                result.Message = Messages.Failed; 
                return result;
            }

            var currentUserId = await GetCurrentUserIdAsync();

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                lesson.Status = LessonStatus.Cancelled;
                lesson.CancellationReason = input.Reason;
                SetUpdatedFields(lesson, currentUserId);
                _context.Lessons.Update(lesson);
                SetEntityModifiedFields(lesson);

                var enrolledStudentUserIds = await _context.Enrollments
                    .Where(e => (lesson.GroupId != null && e.GroupId == lesson.GroupId)
                             || (lesson.CourseId != null && e.CourseId == lesson.CourseId))
                    .Where(e => e.Status == EnrollmentStatus.Active)
                    .Join(_context.Students, e => e.StudentId, s => s.Id, (e, s) => s)
                    .ToListAsync();

                
                foreach (var _ in enrolledStudentUserIds)
                {
                    
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                result.Success = true;
                result.Message = Messages.Success;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                result.Message = Messages.Failed;
            }

            return result;
        }

        public async Task<List<LessonScheduleRowDto>> GetScheduleAsync(int? courseId, int? groupId)
        {
            var query = _context.Lessons
                .Include(l => l.Group).ThenInclude(g => g.Course)
                .Include(l => l.Course)
                .AsQueryable();

            if (courseId != null) query = query.Where(l => l.CourseId == courseId);
            if (groupId != null) query = query.Where(l => l.GroupId == groupId);

            var lessons = await query.OrderBy(l => l.ScheduledDate).ThenBy(l => l.StartTime).ToListAsync();

            return lessons.Select(l =>
            {
                var deliveryType = l.Course?.DeliveryType ?? l.Group?.Course?.DeliveryType;
                return new LessonScheduleRowDto
                {
                    LessonId = l.Id,
                    CourseType = deliveryType,
                    Topic = l.Title,
                    Date = l.ScheduledDate,
                    Day = l.ScheduledDate.DayOfWeek,
                    StartTime = l.StartTime,
                    DurationMinutes = l.DurationMinutes,
                    PlatformOrRoom = deliveryType == CourseDeliveryType.Online
                        ? $"{l.MeetingPlatform} — {l.MeetingUrl}"
                        : l.Room ?? "—"
                };
            }).ToList();
        }
    }
}