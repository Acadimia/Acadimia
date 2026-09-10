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
    }
}