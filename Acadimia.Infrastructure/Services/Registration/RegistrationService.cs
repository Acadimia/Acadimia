using Acadimia.Data.DbContext;
using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Acadimia.Infrastructure.Services.Registration
{
    public interface IRegistrationService
    {
        Task<(OperationResult Result, User? User)> RegisterAsync(RegisterDto input);
    }

    public class RegistrationService : IRegistrationService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegistrationService> _logger;

        public RegistrationService(ApplicationDbContext context, UserManager<User> userManager,
            ILogger<RegistrationService> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<(OperationResult Result, User? User)> RegisterAsync(RegisterDto input)
        {
            var fail = new OperationResult(false, Messages.Invalid);

            // Only self-registration types; Admin can never be requested.
            var userTypeId = input.UserTypeId ?? UserTypeIds.Student;
            if (!UserTypeIds.SelfRegistration.Contains(userTypeId)) return (fail, null);

            var needsProfile = userTypeId == UserTypeIds.Student || userTypeId == UserTypeIds.Teacher;
            if (userTypeId == UserTypeIds.Student)
            {
                if (input.GradeId == null || !await _context.Grades.AnyAsync(g => g.Id == input.GradeId))
                { fail.Message = "الصف الدراسي مطلوب"; return (fail, null); }
            }

            if (await _userManager.FindByNameAsync(input.Email) != null)
            { fail.Message = Messages.UniqueEmail; return (fail, null); }

            if (await _context.Users.AnyAsync(u => u.PhoneNumber == input.PhoneNumber))
            { fail.Message = Messages.UniquePhoneNo; return (fail, null); }

            var userId = Guid.NewGuid().ToString();
            var strategy = _context.Database.CreateExecutionStrategy();

            try
            {
                return await strategy.ExecuteAsync(async () =>
                {
                    _context.ChangeTracker.Clear(); // clean state on every retry
                    var attempt = new OperationResult(false, Messages.Invalid);

                    await using var tx = await _context.Database.BeginTransactionAsync();

                    var user = new User
                    {
                        Id = userId,
                        Name = input.Name,
                        Email = input.Email,
                        UserName = input.Email,
                        PhoneNumber = input.PhoneNumber,
                        GenderId = input.GenderId,
                        UserTypeId = userTypeId,
                        IsActive = true,
                        Avatar = "default_avatar.png",
                        CreatedOn = DateTime.Now
                    };

                    // Password is hashed by Identity; nothing is stored in plaintext.
                    var create = await _userManager.CreateAsync(user, input.Password);
                    if (!create.Succeeded)
                    {
                        await tx.RollbackAsync();
                        attempt.Message = string.Join("<br>", create.Errors.Select(e => e.Description));
                        return (attempt, (User?)null);
                    }

                    _context.Wallets.Add(new Wallet { UserId = userId, Balance = 0, CreatedBy = userId, CreatedOn = DateTime.Now });

                    if (userTypeId == UserTypeIds.Student)
                    {
                        _context.Students.Add(new Student
                        {
                            UserId = userId,
                            Name = input.Name,
                            WhatsAppNumber = input.PhoneNumber,
                            GradeId = input.GradeId!.Value,
                            CreatedBy = userId,
                            CreatedOn = DateTime.Now
                        });
                    }
                    else if (userTypeId == UserTypeIds.Teacher)
                    {
                        _context.Teachers.Add(new Teacher
                        {
                            UserId = userId,
                            GradeId = null,  
                            CreatedBy = userId,
                            CreatedOn = DateTime.Now
                        });
                    }

                    // Any failure here throws; the transaction is disposed without commit => full rollback,
                    // so no orphan user, wallet or profile is left behind.
                    await _context.SaveChangesAsync();
                    await tx.CommitAsync();

                    attempt.Success = true;
                    attempt.Message = Messages.Success;
                    return (attempt, user);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed for {Email}", input.Email);
                fail.Message = Messages.Failed;
                return (fail, null);
            }
        }
    }
}