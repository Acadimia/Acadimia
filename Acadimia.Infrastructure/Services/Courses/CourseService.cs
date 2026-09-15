using Acadimia.Data.Enums;
using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Courses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Infrastructure.Services.Courses
{
    public class CourseService : BaseService, ICourseService
    {
        private static readonly HashSet<string> AllowedSortColumns = new(StringComparer.OrdinalIgnoreCase)
        {
            "Id", "Title", "Price", "Status", "DeliveryType", "CreatedOn"
        };

        public CourseService(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
            : base(context, userManager, httpContextAccessor)
        {
        }

        public async Task<PagedResultDto<List<Course>>> GetAllAsync(PagedResultRequestDto<Course> input)
        {
            IQueryable<Course> courses = _context.Courses
                .Include(c => c.Teacher).Include(c => c.Subject).Include(c => c.Category)
                .Where(c => input.SearchValue.TeacherId == 0 || c.TeacherId == input.SearchValue.TeacherId);

            courses = courses.ApplySort(input.SortColumn, input.SortColumnDirection, AllowedSortColumns, defaultSort: "Id desc");

            return new PagedResultDto<List<Course>>
            {
                Data = await courses.Skip(input.Skip).Take(input.PageSize).ToListAsync(),
                TotalCount = await courses.CountAsync()
            };
        }
        public async Task<Course> GetByIdOrDefaultAsync(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher).Include(c => c.Subject).Include(c => c.Category)
                .SingleOrDefaultAsync(c => c.Id == id);

            return course ?? new Course();
        }

        public async Task<OperationResult> CreateEditAsync(CourseInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);
            var currentUserId = await GetCurrentUserIdAsync();

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Course course;

                if (input.Id == 0)
                {
                    course = new Course
                    {
                        TeacherId = input.TeacherId,
                        SubjectId = input.SubjectId,
                        CategoryId = input.CategoryId,
                        Title = input.Title,
                        Description = input.Description,
                        Price = input.Price,
                        DeliveryType = input.DeliveryType,
                        MaxStudents = input.MaxStudents,
                        Status = input.SaveAsDraft ? CourseStatus.Draft : CourseStatus.Published
                    };
                    SetCreatedFields(course, currentUserId);
                    await _context.Courses.AddAsync(course);
                    await _context.SaveChangesAsync(); 

                    var group = new Group
                    {
                        Name = input.GroupName,
                        GradeId = input.GradeId,
                        TeacherId = input.TeacherId,
                        CourseId = course.Id,
                        MaxStudents = input.MaxStudents,
                        DefaultLessonDurationMinutes = 60 
                    };
                    SetCreatedFields(group, currentUserId);
                    await _context.Groups.AddAsync(group);
                }
                else
                {
                    course = await _context.Courses.SingleOrDefaultAsync(c => c.Id == input.Id);
                    if (course == null)
                    {
                        result.Message = Messages.Failed;
                        return result;
                    }

                    course.SubjectId = input.SubjectId;
                    course.CategoryId = input.CategoryId;
                    course.Title = input.Title;
                    course.Description = input.Description;
                    course.Price = input.Price;
                    course.MaxStudents = input.MaxStudents;
                    if (!input.SaveAsDraft && course.Status == CourseStatus.Draft)
                        course.Status = CourseStatus.Published;

                    SetUpdatedFields(course, currentUserId);
                    _context.Courses.Update(course);
                    SetEntityModifiedFields(course);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                result.Success = true;
                result.Message = Messages.Success;
                result.ReturnId = course.Id;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                result.Message = Messages.Failed;
            }

            return result;
        }

        public async Task<OperationResult> ConfigureGroupScheduleAsync(GroupScheduleInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var group = await _context.Groups.SingleOrDefaultAsync(g => g.Id == input.GroupId);
            if (group == null)
            {
                result.Message = Messages.Failed;
                return result;
            }

  
            foreach (var day in input.ScheduleDays)
            {
                var conflict = await _context.GroupScheduleDays
                    .Include(s => s.Group)
                    .AnyAsync(s => s.Group.TeacherId == group.TeacherId
                                && s.GroupId != group.Id
                                && s.DayOfWeek == day.DayOfWeek
                                && s.StartTime == day.StartTime);

                if (conflict)
                {
                    result.Message = "الجدول يتعارض مع مجموعة أخرى لنفس المعلم"; // schedule conflicts with another group for this teacher
                    return result;
                }
            }

            var currentUserId = await GetCurrentUserIdAsync();

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                group.MaxStudents = input.MaxStudents;
                group.CourseStartDate = input.CourseStartDate;
                group.CourseEndDate = input.CourseEndDate;
                group.DefaultLessonDurationMinutes = input.DefaultLessonDurationMinutes;
                SetUpdatedFields(group, currentUserId);
                _context.Groups.Update(group);
                SetEntityModifiedFields(group);

                var existingDays = await _context.GroupScheduleDays.Where(s => s.GroupId == group.Id).ToListAsync();
                _context.GroupScheduleDays.RemoveRange(existingDays);

                foreach (var day in input.ScheduleDays)
                {
                    await _context.GroupScheduleDays.AddAsync(new GroupScheduleDay
                    {
                        GroupId = group.Id,
                        DayOfWeek = day.DayOfWeek,
                        StartTime = day.StartTime
                    });
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

        public async Task<List<StudentGroupRosterDto>> GetGroupStudentsAsync(int groupId, string? keyword)
        {
            var query = _context.Enrollments
                .Where(e => e.GroupId == groupId && e.Status == EnrollmentStatus.Active)
                .Select(e => e.Student);

            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(s => s.Name.Contains(keyword) || s.WhatsAppNumber.Contains(keyword));

            return await query.Select(s => new StudentGroupRosterDto
            {
                StudentId = s.Id,
                Name = s.Name,
                PhoneNumber = s.WhatsAppNumber,
                Location = s.Location
            }).ToListAsync();
        }
    }
}