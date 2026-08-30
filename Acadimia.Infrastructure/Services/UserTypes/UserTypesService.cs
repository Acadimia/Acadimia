using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Infrastructure.Services.UserTypes
{
    public class UserTypesService : BaseService, IUserTypesService
	{
		public UserTypesService(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
			: base(context, userManager, httpContextAccessor)
		{
		}

		public async Task<PagedResultDto<List<UserType>>> GetAllAsync(PagedResultRequestDto<UserType> input)
		{
			IQueryable<UserType> userTypes = _context.UserTypes
				.Where(x => string.IsNullOrEmpty(input.SearchValue.Keyword)
				? true : (x.Name.Contains(input.SearchValue.Keyword)));
				

			if (!(string.IsNullOrEmpty(input.SortColumn) && string.IsNullOrEmpty(input.SortColumnDirection)))
				userTypes = userTypes.OrderBy(string.Concat(input.SortColumn, " ", input.SortColumnDirection));

			return new PagedResultDto<List<UserType>>()
			{
				Data = await userTypes.Skip(input.Skip).Take(input.PageSize).ToListAsync(),
				TotalCount = await userTypes.CountAsync()
			};
		}

        public async Task<UserType> GetByIdOrDefaultAsync(int id)
		{
            var userType = await _context.UserTypes.SingleOrDefaultAsync(x => x.Id == id);
            if (userType != null)
                return userType;

            return new UserType();
		}

		public async Task<OperationResult> CreateEditAsync(UserType input)
        {
			var result = new OperationResult();
            try
            {
				var currentUserId = await GetCurrentUserIdAsync();

				if (input.Id == 0)
                {
                    SetCreatedFields(input, currentUserId);
                    await _context.UserTypes.AddAsync(input);
                }
                else
                {
                   SetUpdatedFields(input, currentUserId);
                    _context.UserTypes.Update(input);
                    SetEntityModifiedFields(input);
                }

                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = Messages.Success;
            }
            catch (Exception ex)
            {
                result.Message = GetUniqueConstraintMessage(ex, new System.Collections.Generic.Dictionary<string, string>
                {
                    { "uniquename", Messages.UniqueName }
                });
            }
            return result;
        }

        public async Task<OperationResult> DeleteAsync(int id)
		{ 
            var result = new OperationResult();
			var userType = await _context.UserTypes.SingleOrDefaultAsync(x => x.Id == id);
			if (userType != null)
			{
				userType.IsDeleted = true;
				userType.DeletedBy = await GetCurrentUserIdAsync();

                userType.Name += $"_{Guid.NewGuid()}";
                _context.UserTypes.Update(userType);

                // Find and delete all permissions associated with this user type
                var userTypPermissions = await _context.UserPermissions.Where(up => up.UserTypeId == userType.Id).ToListAsync();
                
                if(userTypPermissions.Any())
                    _context.UserPermissions.RemoveRange(userTypPermissions);

                await _context.SaveChangesAsync();

                result.Success  = true;
                result.Message = Messages.Success;
			}
			return result;
		}
	}
}
