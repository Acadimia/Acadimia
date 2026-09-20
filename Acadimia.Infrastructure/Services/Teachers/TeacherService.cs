using Acadimia.Data.Enums;
using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Teachers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Infrastructure.Services.Teachers
{
    public class TeacherService : BaseService, ITeacherService
    {
        
        private static readonly HashSet<string> AllowedSortColumns = new(StringComparer.OrdinalIgnoreCase)
        {
            "Id", "GradeId", "CreatedOn"
        };

        public TeacherService(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
            : base(context, userManager, httpContextAccessor)
        {
        }

        public async Task<PagedResultDto<List<Teacher>>> GetAllAsync(PagedResultRequestDto<Teacher> input)
        {
            IQueryable<Teacher> teachers = _context.Teachers.Include(t => t.Grade);

            if (input.SearchValue?.GradeId > 0)
                teachers = teachers.Where(t => t.GradeId == input.SearchValue.GradeId);

            teachers = teachers.ApplySort(input.SortColumn, input.SortColumnDirection, AllowedSortColumns, defaultSort: "Id desc");

            return new PagedResultDto<List<Teacher>>
            {
                Data = await teachers.Skip(input.Skip).Take(input.PageSize).ToListAsync(),
                TotalCount = await teachers.CountAsync()
            };
        }

        public async Task<Teacher> GetByIdOrDefaultAsync(int id)
        {
            var teacher = await _context.Teachers.Include(t => t.Grade).SingleOrDefaultAsync(t => t.Id == id);
            return teacher ?? new Teacher();
        }

        public async Task<OperationResult> CreateEditAsync(TeacherInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);
            try
            {
                var currentUserId = await GetCurrentUserIdAsync();

                if (input.Id == 0)
                {
                    var teacher = new Teacher
                    {
                        GradeId = input.GradeId
                    };
                    SetCreatedFields(teacher, currentUserId);
                    await _context.Teachers.AddAsync(teacher);
                }
                else
                {
                    var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.Id == input.Id);
                    if (teacher == null)
                    {
                        result.Message = Messages.Failed;
                        return result;
                    }

                    teacher.GradeId = input.GradeId;
                    SetUpdatedFields(teacher, currentUserId);
                    _context.Teachers.Update(teacher);
                    SetEntityModifiedFields(teacher);
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

        public async Task<OperationResult> DeleteAsync(int id)
        {
            var result = new OperationResult(false, Messages.Failed);

            var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.Id == id);
            if (teacher == null)
                return result;

            
            var hasCourses = await _context.Courses.AnyAsync(c => c.TeacherId == id);
            var hasGroups = await _context.Groups.AnyAsync(g => g.TeacherId == id);
            if (hasCourses || hasGroups)
            {
                result.Message = Messages.Failed; 
                return result;
            }

            teacher.IsDeleted = true;
            teacher.DeletedBy = await GetCurrentUserIdAsync();
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();

            result.Success = true;
            result.Message = Messages.Success;
            return result;
        }

        public async Task<List<Grade>> GetGradesListAsync()
        {
            return await _context.Grades
                .Select(g => new Grade { Id = g.Id, Name = g.Name, Section = g.Section })
                .ToListAsync();
        }

        public async Task<PagedResultDto<List<TeacherProfileDto>>> SearchAsync(TeacherSearchFilterDto filter)
        {
            IQueryable<Teacher> query = _context.Teachers.Include(t => t.User).Where(t => t.IsPublicForDiscovery);

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
                query = query.Where(t => t.User.Name.Contains(filter.Keyword));

            if (filter.SubjectId.HasValue)
                query = query.Where(t => _context.Set<TeacherSubject>().Any(ts => ts.TeacherId == t.Id && ts.SubjectId == filter.SubjectId));

            if (filter.GradeId.HasValue)
                query = query.Where(t => _context.Set<TeacherGradeLevel>().Any(tg => tg.TeacherId == t.Id && tg.GradeId == filter.GradeId));

            if (!string.IsNullOrWhiteSpace(filter.ServiceArea))
                query = query.Where(t => t.ServiceArea != null && t.ServiceArea.Contains(filter.ServiceArea));

            if (filter.Online == true) query = query.Where(t => t.SupportsOnline);
            if (filter.InPerson == true) query = query.Where(t => t.SupportsInPerson);

            if (filter.MinPrice.HasValue)
                query = query.Where(t => (t.HourlyPriceOnline ?? 0) >= filter.MinPrice || (t.HourlyPriceInPerson ?? 0) >= filter.MinPrice);
            if (filter.MaxPrice.HasValue)
                query = query.Where(t => (t.HourlyPriceOnline ?? decimal.MaxValue) <= filter.MaxPrice || (t.HourlyPriceInPerson ?? decimal.MaxValue) <= filter.MaxPrice);

            if (filter.MinExperienceYears.HasValue)
                query = query.Where(t => t.ExperienceYears >= filter.MinExperienceYears);

            if (!string.IsNullOrWhiteSpace(filter.Language))
                query = query.Where(t => t.Languages != null && t.Languages.Contains(filter.Language));

            if (filter.AvailableDay.HasValue)
                query = query.Where(t => _context.Set<TeacherAvailability>().Any(a => a.TeacherId == t.Id && a.IsActive && a.DayOfWeek == filter.AvailableDay));

            var totalCount = await query.CountAsync();
            var teachers = await query.Skip(filter.Skip).Take(filter.PageSize).ToListAsync();
            var teacherIds = teachers.Select(t => t.Id).ToList();

            var subjectsByTeacher = await _context.Set<TeacherSubject>().Where(ts => teacherIds.Contains(ts.TeacherId)).Include(ts => ts.Subject).ToListAsync();
            var gradesByTeacher = await _context.Set<TeacherGradeLevel>().Where(tg => teacherIds.Contains(tg.TeacherId)).Include(tg => tg.Grade).ToListAsync();
            var ratingsByTeacher = await _context.Set<TeacherRating>().Where(r => teacherIds.Contains(r.TeacherId))
                .GroupBy(r => r.TeacherId)
                .Select(g => new { TeacherId = g.Key, Avg = g.Average(r => r.RatingValue), Count = g.Count() })
                .ToListAsync();

            var dtos = teachers.Select(t => new TeacherProfileDto
            {
                Id = t.Id,
                Name = t.User?.Name,
                Bio = t.Bio,
                Qualifications = t.Qualifications,
                ExperienceYears = t.ExperienceYears,
                ServiceArea = t.ServiceArea,
                Languages = t.Languages,
                SupportsOnline = t.SupportsOnline,
                SupportsInPerson = t.SupportsInPerson,
                HourlyPriceOnline = t.HourlyPriceOnline,
                HourlyPriceInPerson = t.HourlyPriceInPerson,
                ProfileImage = t.ProfileImage,
                Subjects = subjectsByTeacher.Where(ts => ts.TeacherId == t.Id).Select(ts => ts.Subject.Name).ToList(),
                Grades = gradesByTeacher.Where(tg => tg.TeacherId == t.Id).Select(tg => tg.Grade.Name).ToList(),
                AverageRating = ratingsByTeacher.FirstOrDefault(r => r.TeacherId == t.Id)?.Avg,
                RatingCount = ratingsByTeacher.FirstOrDefault(r => r.TeacherId == t.Id)?.Count ?? 0
            }).ToList();

            if (filter.MinRating.HasValue)
                dtos = dtos.Where(d => (d.AverageRating ?? 0) >= filter.MinRating).ToList();

            return new PagedResultDto<List<TeacherProfileDto>> { Data = dtos, TotalCount = totalCount };
        }

        public async Task<TeacherProfileDto?> GetPublicProfileAsync(int teacherId)
        {
            var teacher = await _context.Teachers.Include(t => t.User)
        .SingleOrDefaultAsync(t => t.Id == teacherId && t.IsPublicForDiscovery);
            if (teacher == null) return null;

            var subjects = await _context.Set<TeacherSubject>().Where(ts => ts.TeacherId == teacherId).Include(ts => ts.Subject).ToListAsync();
            var grades = await _context.Set<TeacherGradeLevel>().Where(tg => tg.TeacherId == teacherId).Include(tg => tg.Grade).ToListAsync();
            var ratings = await _context.Set<TeacherRating>().Where(r => r.TeacherId == teacherId).ToListAsync();

            return new TeacherProfileDto
            {
                Id = teacher.Id,
                Name = teacher.User?.Name,
                Bio = teacher.Bio,
                Qualifications = teacher.Qualifications,
                ExperienceYears = teacher.ExperienceYears,
                ServiceArea = teacher.ServiceArea,
                Languages = teacher.Languages,
                SupportsOnline = teacher.SupportsOnline,
                SupportsInPerson = teacher.SupportsInPerson,
                HourlyPriceOnline = teacher.HourlyPriceOnline,
                HourlyPriceInPerson = teacher.HourlyPriceInPerson,
                ProfileImage = teacher.ProfileImage,
                Subjects = subjects.Select(s => s.Subject.Name).ToList(),
                Grades = grades.Select(g => g.Grade.Name).ToList(),
                AverageRating = ratings.Any() ? ratings.Average(r => r.RatingValue) : null,
                RatingCount = ratings.Count
            };
        }
       
        public async Task<OperationResult> UpdateProfileAsync(string userId, TeacherProfileInputDto input)
        {
            var result = new OperationResult(false, Messages.Failed);

            var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.UserId == userId);
            if (teacher == null) return result; // caller isn't a teacher

            teacher.Bio = input.Bio;
            teacher.Qualifications = input.Qualifications;
            teacher.ExperienceYears = input.ExperienceYears;
            teacher.ServiceArea = input.ServiceArea;
            teacher.Languages = input.Languages;
            teacher.SupportsOnline = input.SupportsOnline;
            teacher.SupportsInPerson = input.SupportsInPerson;
            teacher.HourlyPriceOnline = input.HourlyPriceOnline;
            teacher.HourlyPriceInPerson = input.HourlyPriceInPerson;
            teacher.IsPublicForDiscovery = input.IsPublicForDiscovery;

            SetUpdatedFields(teacher, userId);
            _context.Teachers.Update(teacher);
            SetEntityModifiedFields(teacher);

            var existingSubjects = await _context.Set<TeacherSubject>().Where(ts => ts.TeacherId == teacher.Id).ToListAsync();
            _context.RemoveRange(existingSubjects.Where(ts => !input.SubjectIds.Contains(ts.SubjectId)));
            foreach (var subjectId in input.SubjectIds.Except(existingSubjects.Select(ts => ts.SubjectId)))
                await _context.AddAsync(new TeacherSubject { TeacherId = teacher.Id, SubjectId = subjectId });

            var existingGrades = await _context.Set<TeacherGradeLevel>().Where(tg => tg.TeacherId == teacher.Id).ToListAsync();
            _context.RemoveRange(existingGrades.Where(tg => !input.GradeIds.Contains(tg.GradeId)));
            foreach (var gradeId in input.GradeIds.Except(existingGrades.Select(tg => tg.GradeId)))
                await _context.AddAsync(new TeacherGradeLevel { TeacherId = teacher.Id, GradeId = gradeId });

            await _context.SaveChangesAsync();
            result.Success = true;
            result.Message = Messages.Success;
            return result;
        }

        public async Task<OperationResult> SetAvailabilityAsync(string userId, TeacherAvailabilityInputDto input)
        {
            var result = new OperationResult(false, Messages.Failed);

            var teacher = await _context.Teachers.SingleOrDefaultAsync(t => t.UserId == userId);
            if (teacher == null) return result;

            // FR-T09 exception flow: reject if new availability drops a slot with an existing confirmed booking.
            foreach (var slot in input.Slots)
            {
                var hasConflictingBooking = await _context.Bookings.AnyAsync(b =>
                    b.TeacherId == teacher.Id &&
                    b.Date.DayOfWeek == slot.DayOfWeek &&
                    (b.Status == BookingStatus.Accepted || b.Status == BookingStatus.Confirmed) &&
                    b.StartTime < slot.EndTime &&
                    slot.StartTime < b.StartTime.Add(TimeSpan.FromMinutes(b.DurationMinutes)));

                if (hasConflictingBooking)
                {
                    result.Message = "التوفر الجديد يتعارض مع حجز مؤكد قائم";
                    return result;
                }
            }

            var existing = await _context.Set<TeacherAvailability>().Where(a => a.TeacherId == teacher.Id).ToListAsync();
            _context.RemoveRange(existing);

            foreach (var slot in input.Slots)
            {
                var availability = new TeacherAvailability
                {
                    TeacherId = teacher.Id,
                    DayOfWeek = slot.DayOfWeek,
                    StartTime = slot.StartTime,
                    EndTime = slot.EndTime,
                    TeachingMode = slot.TeachingMode,
                    EffectiveFrom = slot.EffectiveFrom,
                    EffectiveTo = slot.EffectiveTo,
                    IsActive = true
                };
                SetCreatedFields(availability, userId);
                await _context.Set<TeacherAvailability>().AddAsync(availability);
            }

            await _context.SaveChangesAsync();
            result.Success = true;
            result.Message = Messages.Success;
            return result;
        }

        public async Task<List<TeacherAvailability>> GetAvailabilityAsync(int teacherId, CourseDeliveryType? mode, DateTime? date)
        {
            var query = _context.Set<TeacherAvailability>().Where(a => a.TeacherId == teacherId && a.IsActive);

            if (mode.HasValue)
                query = query.Where(a => a.TeachingMode == mode);

            if (date.HasValue)
                query = query.Where(a => a.DayOfWeek == date.Value.DayOfWeek
                    && (a.EffectiveFrom == null || a.EffectiveFrom <= date)
                    && (a.EffectiveTo == null || a.EffectiveTo >= date));

            return await query.OrderBy(a => a.DayOfWeek).ThenBy(a => a.StartTime).ToListAsync();
        }
    }
}